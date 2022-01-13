using System.Web.Script.Serialization;

namespace EpointAutomationHelper
{
    /// <summary>
    /// Json工具类
    /// </summary>
    public class JsonHelper
    {
        
        #region 操作json数据
        /// <summary>
        /// 编码json数据
        /// </summary>
        /// <param name="objectToSerialize">编码对象</param>
        /// <returns></returns>
        public static string DumpToJson(object objectToSerialize)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Serialize(objectToSerialize);
        }

        /// <summary>
        /// 解码json数据
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="jsonString">json数据</param>
        /// <returns></returns>
        public static T DecodeJson<T>(string jsonString)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Deserialize<T>(jsonString);
        }
        #endregion
    }
}
