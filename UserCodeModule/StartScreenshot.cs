/*
 * Created by Ranorex
 * User: kissKurisu
 * Date: 2018/2/6
 * Time: 15:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using Ranorex;
using Ranorex.Core.Testing;

namespace EpointAutomationHelper.UserCodeModule
{
    /// <summary>
    /// 定时截图，并清理
    /// </summary>
    [TestModule("5457883C-D99A-427A-91CC-5E5F5AAE67A4", ModuleType.UserCode, 1)]
    public class StartScreenshot : ITestModule
    {
        /// <summary>
        /// 定时截图，并清理
        /// </summary>
        public StartScreenshot()
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
            ////使用System.Threading.Timer
            //var autoEvent = new AutoResetEvent(false);
            //System.Threading.Timer ttimer = new Timer(new TimerCallback(ReportHelper.TakeScreenshot), autoEvent, 0, 500);

            //定时截图
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 500; //执行间隔时间,单位为毫秒; 同步修改GenerateGif中的时间
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(ReportHelper.CaptureScreenshot);

            //定时清理截图
            System.Timers.Timer timer2 = new System.Timers.Timer();
            timer2.Enabled = true;
            timer2.Interval = 180000; //执行间隔时间,单位为毫秒;3分钟 
            timer2.Start();
            timer2.Elapsed += new System.Timers.ElapsedEventHandler(ReportHelper.CleanUp);

            ////定时监视测试状态
            //System.Timers.Timer timer3 = new System.Timers.Timer();
            //timer3.Enabled = true;
            //timer3.Interval = 5000; //执行间隔时间,单位为毫秒; 
            //timer3.Start();
            //timer3.Elapsed += new System.Timers.ElapsedEventHandler(ReportHelper.FalureGif);

            Delay.Milliseconds(1500);
            Report.Info("CaptureScreenshot started.");
        }
    }
}
