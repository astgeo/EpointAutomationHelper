/*
 * Created by Ranorex
 * User: Administrator
 * Date: 2022/1/10
 * Time: 13:46
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
    /// Description of TestIDNumGen.
    /// </summary>
    [TestModule("3C77C17B-2891-4578-8A67-DAE7A8F60773", ModuleType.UserCode, 1)]
    public class TestIDNumGen : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public TestIDNumGen()
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
        	var example = gen.RandomIDNum();
        	IDNumGen che = new IDNumGen();
        	Report.Info(string.Format("随机生成身份证号:" + example));
        	Report.Info(string.Format("验证随机生成的身份证号:" + Convert.ToString(che.checkIDNum(example))));
        	
		}
    }
}
