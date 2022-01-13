/*
 * Created by Ranorex
 * User: taurus
 * Date: 2018-11-30
 * Time: 15:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Ranorex;
using Ranorex.Core.Testing;
using System;
using System.Diagnostics;

namespace EpointAutomationHelper.UserCodeModule
{
	/// <summary>
	/// 使用TravisCI发送邮件
	/// </summary>
	[TestModule("1BB8F453-A3DF-42E9-8EDC-00C53FE8A666", ModuleType.UserCode, 1)]
	public class SendEmailByTravisCI : ITestModule
	{
		/// <summary>
		/// 使用TravisCI发送邮件
		/// </summary>
		public SendEmailByTravisCI()
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

			Report.Info("=====使用TravisCI发送邮件=====");
			Delay.Milliseconds(1000);
            string batPath = DirHelper.BaseDirectory + @"\Data\AutoPush.bat";
			CmdHelper.RunBat(batPath);
		}
	}
}
