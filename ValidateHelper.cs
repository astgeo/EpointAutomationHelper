using Ranorex;

namespace EpointAutomationHelper
{
    /// <summary>
    /// Validate工具类
    /// </summary>
    public class ValidateHelper
    {
        /// <summary>
        /// 跟在自定义信息后输出的内容
        /// </summary>
        private const string SUFFIX_STRING = "  : Objects are @ValidateNOT@equal (actual='{0}', expected='{1}').";

        /// <summary>
        /// 比对两个值是否相等
        /// </summary>
        /// <param name="actual">实际值</param>
        /// <param name="expected">预期值</param>
        /// <param name="message">输出信息</param>
        public static void AreEqual(object actual, object expected, string message)
        {
            ValidateHelper.AreEqual(actual, expected, message, true);
        }

        /// <summary>
        /// 比对两个值是否相等
        /// </summary>
        /// <param name="actual">实际值</param>
        /// <param name="expected">预期值</param>
        /// <param name="message">输出信息</param>
        /// <param name="exceptionOnFail">验证失败是否继续执行</param>
        /// <returns></returns>
        public static bool AreEqual(object actual, object expected, string message, bool exceptionOnFail)
        {
            bool result = false;
            try
            {
                result = Validate.AreEqual(actual, expected, message + SUFFIX_STRING, new Validate.Options(exceptionOnFail));
                if (!result)
                {
                    //Report.Info("ValidateFailure-try");
                    ReportHelper.GenerateScreencast();
                }
            }
            catch (ValidationException ex)
            {
                //Report.Info("ValidateFailure-catch");
                ReportHelper.GenerateScreencast();
                throw ex;
            }
            return result;
        }
    }
}
