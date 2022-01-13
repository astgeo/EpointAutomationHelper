using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Ranorex.Core;
using Ranorex.Core.Testing;
using Ranorex.Core.Reporting;

namespace EpointAutomationHelper
{
    public class FakeDataHelper
    {
        /// <summary>
        /// 随机生成身份证号码,出生年份为80年前至25年前,含80
        /// </summary>
        /// <returns>身份证号码</returns>
        public string RandomIDNum()
        {
            IDNumGen gen = new IDNumGen();
            return gen.randomIDNum();
        }

        /// <summary>
        /// 随机生成身份证号码,可限定年龄范围
        /// </summary>
        /// <param name="min">年龄最小值(不含)</param>
        /// <param name="max">年龄最大值(含)</param>
        /// <returns>身份证号码</returns>
        public string RandomIDNum(int min, int max)
        {
            IDNumGen gen = new IDNumGen();
            return gen.randomIDNum(min, max);
        }
    }
}
