using Ranorex;

namespace EpointAutomationHelper
{
    /// <summary>
    /// 文件选择工具类
    /// </summary>
    public class FileDialogHelper
    {
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="dialogTitle">打开对话框标题（Spy抓出的Title值），默认：打开</param>
        /// <param name="btnOpenText">打开按钮文本（Spy抓出的Text值），默认：打开(O)</param>
        /// <param name="isFuzzy">是否模糊查询，默认false（包括标题和打开按钮）</param>
        public static void OpenFile(string filePath, string dialogTitle="打开", string btnOpenText= "打开(&O)", bool isFuzzy = false)
        {
            string btnPath;
            if (isFuzzy)
            {
                //模糊查询按钮
                btnPath = "/form[@title~'" + dialogTitle + "']/button[@text~'" + btnOpenText + "']";
            }
            else
            {
                //精确查询按钮
                btnPath = "/form[@title='" + dialogTitle + "']/button[@text='" + btnOpenText + "']";
            }

            Button btnOpen;
            //定位打开按钮
            bool found = Host.Local.TryFindSingle(btnPath, 30000, out btnOpen);
            if (found)
            {
                SetValueFilePath(filePath, dialogTitle, isFuzzy);
                btnOpen.Click();
            }
            else
            {
                Report.Warn("没有找到窗体title=" + dialogTitle + ",text = " + btnOpenText + "的按钮。");
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="dialogTitle">保存对话框标题</param>
        /// <param name="btnSaveText">保存按钮文本</param>
        public static void SaveFile(string filePath, string dialogTitle, string btnSaveText)
        {
            OpenFile(filePath, dialogTitle, btnSaveText);
        }


        /// <summary>
        /// 打开文件对话框输入文件路径
        /// </summary>
        /// <param name="filePath">需要打开的文件的绝对路径</param>
        /// <param name="dialogTitle">对话框标题</param>
        /// <param name="isFuzzy">是否模糊查询标题，默认false</param>
        public static void SetValueFilePath(string filePath, string dialogTitle, bool isFuzzy = false)
        {
        	string txtPath;
        	if (isFuzzy) 
        	{
        		//模糊查询标题定位到txtPath控件
        		txtPath = "/form[@title~'" + dialogTitle + "']/?/?/text[@controlid='1148']";
        	}
        	else
        	{
        		//精确查询标题定位到txtPath控件
        		txtPath = "/form[@title='" + dialogTitle + "']/?/?/text[@controlid='1148']";
        	}
            
            Text path;
            bool found = Host.Local.TryFindSingle(txtPath, 30000, out path);                  //定位路径文本框
            if (found)
            {
                path.Element.SetAttributeValue("Text", filePath);
            }
            else
            {
                Report.Warn("没有找到窗体title=" + dialogTitle + ",controlid=1148的路径文本框");
            }
        }
    }
}
