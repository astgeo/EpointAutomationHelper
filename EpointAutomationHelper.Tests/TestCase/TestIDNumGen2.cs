/*
 * Created by Ranorex
 * User: Administrator
 * Date: 2022/1/10
 * Time: 14:35
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
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

namespace EpointAutomationHelper.Tests.TestCase
{
    /// <summary>
    /// Description of TestIDNumGen2.
    /// </summary>
    [TestModule("9A83EAC2-E51A-458B-9860-7B34A758181F", ModuleType.UserCode, 1)]
    public class TestIDNumGen2 : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public TestIDNumGen2()
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
            
            RunTestIDNumGen();
        }
                
        private void RunTestIDNumGen()
		{
        	FakeDataHelper gen = new FakeDataHelper();
        	var example2 = gen.RandomIDNum(30,60);
        	IDNumGen che = new IDNumGen();
        	Report.Info(string.Format("随机生成30至60岁间身份证号:" + example2));
        	Report.Info(string.Format("验证生成的30至60岁身份证号:" + Convert.ToString(che.checkIDNum(example2))));
        	
		}
    }
}
