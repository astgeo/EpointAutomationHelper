/*
 * Created by Ranorex
 * User: taurus-master
 * Date: 2020/5/21
 * Time: 20:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
using EpointAutomationHelper.UserCodeModule;

namespace EpointAutomationHelper.Tests.TestCase
{
	/// <summary>
	/// Description of TestSendEmail.
	/// </summary>
	[TestModule("8496F5A8-82DB-4CD2-BDB2-5A1F5B842A28", ModuleType.UserCode, 1)]
	public class TestSendEmail : ITestModule
	{
		/// <summary>
		/// Constructs a new instance.
		/// </summary>
		public TestSendEmail()
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
			
			RunTestSendEmail();
		}
		
		private void RunTestSendEmail()
		{
			try
			{
				Random rn = new Random();
				if (rn.Next(0, 1) ==  0)
				{
					Report.Info("执行异常发送邮件");
					//throw new Exception();
				}
			}
			finally
			{
				//TestModuleRunner.Run(new SendEmailByGithubAction());
				Delay.Milliseconds(1000);
			}
		}
	}
}
