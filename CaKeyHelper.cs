using System;
using System.Diagnostics;

namespace EpointAutomationHelper
{
    /// <summary>
    /// CA锁相关工具类
    /// </summary>
    public class CaKeyHelper
    {

        /// <summary>
        /// CA锁相关无参构造函数
        /// </summary>
        public CaKeyHelper()
        { }

        /// <summary>
        /// 从服务器借锁
        /// </summary>
        /// <param name="keyName">CA锁名称，对应OA“CA锁名称”列</param>
        public static void Borrow(string keyName)
        {
            //位于“测试数据\CA锁借用\AutoTestMonitor.exe”
            Process p = Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"测试数据\CA锁借用\AutoTestMonitor.exe", "startkey " + keyName + "");
            p.WaitForExit(10000);

        }

        /// <summary>
        /// 还锁
        /// </summary>
        /// <param name="keyName">CA锁名称，对应OA“CA锁名称”列</param>
        public static void Return(string keyName)
        {
            //位于“测试数据\CA锁借用\AutoTestMonitor.exe”
            Process p = Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"测试数据\CA锁借用\AutoTestMonitor.exe", "stopkey " + keyName + "");
            p.WaitForExit(10000);
        }
    }
}
