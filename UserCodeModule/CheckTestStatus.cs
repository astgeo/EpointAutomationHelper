/*
 * Created by Ranorex
 * User: kissKurisu
 * Date: 2018/2/7
 * Time: 10:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using Ranorex;
using Ranorex.Core.Testing;

namespace EpointAutomationHelper.UserCodeModule
{
    /// <summary>
    /// 获取当前Test Case或Smart Folder的测试状态，如果为Failed，则截图
    /// </summary>
    [TestModule("FF9B05EC-195D-49D4-9A4C-69632D388C91", ModuleType.UserCode, 1)]
    public class CheckTestStatus : ITestModule
    {
        /// <summary>
        /// 验证测试状态，如果为Failed，则截图
        /// </summary>
        public CheckTestStatus()
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

            //Report.Info("Enter CheckTestStatus");
            //Report.Info(TestSuite.CurrentTestContainer.Name);
            //Report.Info(TestSuite.CurrentTestContainer.Status.ToString());
            if (TestSuite.CurrentTestContainer.Status == Ranorex.Core.Reporting.ActivityStatus.Failed)
            {
                ReportHelper.GenerateScreencast();
            }
        }
    }
}
