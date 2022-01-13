using Ranorex;
using Ranorex.Core;
using System.Drawing;

namespace EpointAutomationHelper
{
    /// <summary>
    /// 图片工具类
    /// </summary>
    public class ImageHelper
    {
        /// <summary>
        /// 在指定控件范围内确定指定图片是否存在
        /// </summary>
        /// <param name="imageFullPath">图片文件路径</param>
        /// <param name="parent">指定控件范围</param>
        /// <param name="similarity">相似度</param>
        /// <param name="timeout">搜索超时时间</param>
        public static bool Exists(string imageFullPath, Element parent, double similarity, Duration timeout)
        {
            Bitmap bmp = Imaging.Load(imageFullPath);
            Imaging.Match match = null;
            return Imaging.TryFindSingle(parent, bmp,
                new Imaging.FindOptions(similarity, Imaging.Preprocessings.None, Rectangle.Empty, true, 10000000.0, timeout), out match);
        }
    }
}
