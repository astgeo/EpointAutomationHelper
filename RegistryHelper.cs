using Microsoft.Win32;
using Ranorex;

namespace EpointAutomationHelper
{
    /// <summary>
    /// 注册表工具类
    /// </summary>
    public class RegistryHelper
    {
        /// <summary>
        /// 读取CLASSES_ROOT下的注册表路径
        /// 多层结构的项名需要完整路径，例如：PDFsamBasic.FileAssoc.Pdf\DefaultIcon
        /// </summary>
        /// <param name="itemName">注册表中项名的完整路径</param>
        /// <returns>返回注册表路径</returns>
        public static string GetPath(string itemName)
        {
            string strKeyName = string.Empty;

            RegistryKey regKey = Registry.ClassesRoot;
            RegistryKey regSubKey = regKey.OpenSubKey(itemName, false);

            object objResult = regSubKey.GetValue(strKeyName);
            RegistryValueKind regValueKind = regSubKey.GetValueKind(strKeyName);
            if (regValueKind == Microsoft.Win32.RegistryValueKind.String)
            {
                Report.Info(objResult.ToString());
            }

            return objResult.ToString();
        }
    }
}
