using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using Ranorex.Core.Reporting;

using System.IO;
using System.Diagnostics;
using System.Xml;

using LibGit2Sharp;

namespace EpointAutomationHelper.UserCodeModule
{
	/// <summary>
	/// Description of SendEmail.
	/// </summary>
	[TestModule("E4933B5D-05A2-44E9-A147-CA134E5F14C4", ModuleType.UserCode, 1)]
	public class SendEmail : ITestModule
	{
		/// <summary>
		/// 邮件相关文件的路径
		/// </summary>
		static string dirPath = DirHelper.BaseDirectory + @"\Data\SendEmail";
		
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public SendEmail()
		{
			// Do not delete - a parameterless constructor is required!
		}

		/// <summary>
		/// Performs the playback of actions in this module.
		/// </summary>
		/// <remarks>You should not call this method directly, instead pass the module
		/// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
		/// that will in turn invoke this method.</remarks>
		void ITestModule.Run()
		{
			Mouse.DefaultMoveTime = 300;
			Keyboard.DefaultKeyPressTime = 100;
			Delay.SpeedFactor = 1.0;
			
			RunSendEmailByGithubAction();
		}
		
		private void RunSendEmailByGithubAction()
		{
			try
			{
				Report.Info("=====开始发送邮件=====");
				CheckDll();
				DirHelper.KillExplorer();
				DirHelper.DeleteFolder(dirPath);
				new GenActionYml(dirPath, GetXmlConfig()).GenYml();
				new GitOperation(dirPath).InitRepo();
				GenTestResult();
				new GitOperation(dirPath).PushRepo();
			}
			catch(System.IO.DirectoryNotFoundException ex)
			{
				Report.Error("请查看EmailData.xml是否添加到项目的Data文件夹中，并设置成复制到输出目录\r\n" + ex.ToString());
			}
			catch(LibGit2Sharp.LibGit2SharpException ex)
			{
				Report.Error(@"请查看1.2.1.527版本的修改纪要文档，安装补丁：EpointAutomationHelper\trunk\Tool\MicrosoftEasyFix51044.msi" + "\r\n" 
				             + ex.ToString());
			}
			catch(Exception ex)
			{
				Report.Error("邮件发送失败\r\n" + ex.ToString());
			}
			finally
			{
				DirHelper.DeleteFolder(dirPath);
			}
		}

		/// <summary>
		/// 生成Push.Bat
		/// </summary>
		private void GenPushBat()
		{
			string[] strs = new string[]
			{
				"Setlocal enabledelayedexpansion",
				"@echo off",
				"pushd \"%~dp0\"",
				"set \"msg=%computername%_Complete_QingBiaoTool_Automation_Test\"",
				"cd SendEmail",
				"git init",
				"git remote add origin git@github.com:taylortaurus/SendEmailByAction.git",
				"git add .",
				"git commit -am %msg%",
				"git push --force origin master",
				"cd ..",
				"rd /q /s SendEmail",
			};
			
			var path = DirHelper.BaseDirectory + @"\Data\Push.bat";
			using (StreamWriter writer = new StreamWriter(path))
			{
				foreach (string s in strs)
				{
					writer.WriteLine(s);
				}
				writer.Close();
			}
		}
		
		/// <summary>
		/// 生成执行结果README.md
		/// </summary>
		private void GenTestResult()
		{
			var path = DirHelper.BaseDirectory + @"\Data\SendEmail\README.md";
			using (StreamWriter writer = new StreamWriter(path))
			{
				var str = string.Format("Machine Name:{0}\r\nProject Name:{1}\r\nTest Result:{2}\r\nTime:{3}",
				                        Environment.MachineName,
				                        Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.FileName),
				                        GetTestSuiteStatus(),
				                        System.DateTime.Now.ToString());
				
				writer.WriteLine(str);
				writer.Close();
			}
		}
		
		/// <summary>
		/// 获取测试套件的执行情况
		/// </summary>
		/// <returns>success or failed</returns>
		private static string GetTestSuiteStatus()
		{
			string status = "";

			var rootChildren = ActivityStack.Instance.RootActivity.Children;

			if (rootChildren.Count > 1)
			{
				Console.WriteLine("Multiple TestSuiteActivites, status taken from first entry");
			}

			var testSuiteAct = rootChildren[0] as TestSuiteActivity;

			if (testSuiteAct != null)
			{
				status = testSuiteAct.Status.ToString();
			}
			
			return status;
		}
		
		/// <summary>
		/// 获取Email.xml配置
		/// </summary>
		/// <returns></returns>
		private string GetXmlConfig()
		{
			string to = "";
			bool isRandomSel = false;
			
			//忽略Xml文件中的注释
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreComments = true;

			XmlDocument doc = new XmlDocument();
			try
			{
				XmlReader reader = XmlReader.Create(DirHelper.BaseDirectory + @"\Data\EmailData.xml",settings);
				doc.Load(reader);
			}
			catch (FileNotFoundException ex)
			{
				Report.Error(ex.Message);
			}

			XmlNode xmlNode = doc.SelectSingleNode("EmailData");
			for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
			{
				var itemName = xmlNode.ChildNodes[i].Attributes["name"].Value;
				if (itemName == "收件箱")
				{
					to = xmlNode.ChildNodes[i].Attributes["value"].Value;
				}
				
				itemName = xmlNode.ChildNodes[i].Attributes["name"].Value;
				if (itemName == "是否随机挑选接收邮箱")
				{
					isRandomSel = Convert.ToBoolean(xmlNode.ChildNodes[i].Attributes["value"].Value);
				}
				
			}
			
			//随机选择邮箱
			if (isRandomSel)
			{
				var arrEmail = to.Split(',');
				Random rn = new Random();
				to = arrEmail[rn.Next(0, arrEmail.Length)];
			}
			
			Report.Info("当前的接收邮箱=" + to);
			return to;
		}
		
		/// <summary>
		/// 验证Dll
		/// </summary>
		private void CheckDll()
		{
			var nativePath = DirHelper.BaseDirectory + @"\NativeBinaries";
			
			if (!Directory.Exists(nativePath))
			{
				Validate.Fail("Dll未配置正确，请查看《修改纪要v1.2.0.526_20200526.docx》进行配置");
			}
		}
	}
}
