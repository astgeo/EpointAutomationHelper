
using Ranorex;
using Ranorex.Core;
using System;

namespace EpointAutomationHelper
{
    /// <summary>
    /// 打开文件操作
    /// </summary>
    [Obsolete("后续版本移除，请改用 FileDialogHelper")]
    public class OpenFileOperation
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public OpenFileOperation()
        {
        }

        /// <summary>
        /// 初始化打开文件操作
        /// </summary>
        /// <param name="formTitle">弹出打开对话框的title</param>
        public OpenFileOperation(string formTitle)
        {
            this.formTitle = formTitle;
        }

        /// <summary>
        /// 初始化打开文件操作
        /// </summary>
        /// <param name="formTitle">弹出打开对话框的title</param>
        /// <param name="btnText">打开按钮的text</param>
        public OpenFileOperation(string formTitle, string btnText)
        {
            this.formTitle = formTitle;
            this.btnText = btnText;
        }

        /// <summary>
        /// 弹出打开对话框的title
        /// </summary>
        private string formTitle = "打开";

        /// <summary>
        /// 打开按钮的text
        /// </summary>
        private string btnText = "打开(&O)";


        /// <summary>
        /// 编写日期：2017-5-9
        /// 编写人：王浩宇
        /// 功能：打开文件对话框输入文件路径后打开文件
        /// </summary>
        /// <param name="filePath">需要打开的文件的绝对路径</param>
        /// <param name="txtPath">路径控件</param>
        /// <param name="btnOpen">打开按钮</param>
        public static void OpenFile(string filePath, Element txtPath, Ranorex.Button btnOpen)
        {
            Set_Value_FilePath(filePath, txtPath);
            btnOpen.Click();
        }

        /// <summary>
        /// 编写日期：2017-5-22
        /// 编写人：王浩宇
        /// 功能：打开文件对话框输入文件路径后打开文件
        /// </summary>
        /// <param name="filePath">需要打开的文件的绝对路径</param>
        public void OpenFile(string filePath)
        {
            string btnPath = "/form[@title='" + formTitle + "']/button[@text='" + btnText + "']";
            Button btnOpen;
            bool found = Host.Local.TryFindSingle(btnPath, 30000, out btnOpen);                   //定位打开按钮
            if (found)
            {
                Set_Value_FilePath(filePath);
                btnOpen.Click();
            }
            else
            {
                Report.Warn("没有找到窗体title=" + formTitle + ",text = " + btnText + "的按钮。");
            }
        }

        /// <summary>
        /// 编写日期：2017-5-9
        /// 编写人：王浩宇
        /// 功能：打开文件对话框输入文件路径
        /// </summary>
        /// <param name="filePath">需要打开的文件的绝对路径</param>
        /// <param name="txtPath">路径控件</param>
        public static void Set_Value_FilePath(string filePath, Element txtPath)
        {
            txtPath.SetAttributeValue("Text", filePath);
        }

        /// <summary>
        /// 编写日期：2017-5-22
        /// 编写人：王浩宇
        /// 功能：打开文件对话框输入文件路径
        /// </summary>
        /// <param name="filePath">需要打开的文件的绝对路径</param>
        public void Set_Value_FilePath(string filePath)
        {
            string txtPath = "/form[@title='" + formTitle + "']/?/?/text[@controlid='1148']";
            Text path;
            bool found = Host.Local.TryFindSingle(txtPath, 30000, out path);                  //定位路径文本框
            if (found)
            {
                path.Element.SetAttributeValue("Text", filePath);
            }
            else
            {
                Report.Warn("没有找到窗体title=" + formTitle + ",controlid=1148的路径文本框");
            }
        }
    }


}
