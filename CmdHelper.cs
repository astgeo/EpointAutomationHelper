using Ranorex;
using System;
using System.Diagnostics;
using System.IO;

namespace EpointAutomationHelper
{
    /// <summary>
    /// Cmd工具类
    /// </summary>
    public class CmdHelper
    {
        /// <summary>
        /// 执行cmd命令
        /// 类似Ping 114.114.114.114 /t，终端不返回结束信息，需要将isWaitConsoleInfo置为false
        /// </summary>
        /// <param name="cmd">cmd命令</param>
        /// <param name="consoleInfo">控制台输出的信息</param>
        /// <param name="isWaitConsoleInfo">是否等待终端返回执行结束的输出信息</param>
        public static void RunCmd(string cmd, out string consoleInfo, bool isWaitConsoleInfo = true)
        {
            string _cmdPath = Constant.CmdPath;
            //不管命令是否成功均执行exit命令，否则当调用ReadToEnd()方法时，会处于假死状态
            cmd = cmd.Trim().TrimEnd('&') + "&exit";
            using (Process p = new Process())
            {
                p.StartInfo.FileName = _cmdPath;
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口写入命令
                p.StandardInput.WriteLine(cmd);
                p.StandardInput.AutoFlush = true;

                //获取cmd窗口的输出信息
                consoleInfo = p.StandardOutput.ReadToEnd().ToString();

                //等待程序执行完退出进程，如果像Ping 114.114.114.114 /t 命令可能会阻塞
                if (isWaitConsoleInfo)
                {
                    p.WaitForExit();
                }
                p.Close();
            }
        }


        /// <summary>
        /// 执行bat文件
        /// </summary>
        /// <param name="batPath">bat的绝对路径</param>
        public static void RunBat(string batPath)
        {
            try
            {
                string targetDir = string.Format(Path.GetDirectoryName(batPath)) ;
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = targetDir;
                proc.StartInfo.FileName = Path.GetFileName(batPath);
                proc.StartInfo.Arguments = string.Format("10");
                //proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//这里设置DOS窗口不显示
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception ex)
            {
                Report.Info(ex.ToString());
            }
        }
    }
}
