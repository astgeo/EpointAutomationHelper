namespace EpointAutomationHelper
{
    /// <summary>
    /// 常量类，放置常量
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// MSBuild路径
        /// </summary>
        public static readonly string MSBuildPath = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe";

        /// <summary>
        /// Cmd.exe文件路径
        /// </summary>
        public static readonly string CmdPath = @"C:\Windows\System32\cmd.exe";
    }

    /// <summary>
    /// 常见的快捷键，可直接在Keyboard.Press()中使用
    /// </summary>
    public class HotKey
    {
        /// <summary>
        /// 【快捷键】全选删除
        /// </summary>
        public static readonly string SelAllAndDel = @"{End}{RShiftKey down}{Home}{RShiftKey up}{Delete}";

        /// <summary>
        /// 【快捷键】Alt+F4关闭
        /// </summary>
        public static readonly string CloseForm = @"{LMenu down}{F4 down}{LMenu up}{F4 up}";
    }
}
