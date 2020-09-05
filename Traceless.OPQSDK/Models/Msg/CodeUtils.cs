using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Msg
{
    public class CodeUtils
    {
        /// <summary>
        /// 获取ATCode，可以实现At列表中的QQ
        /// </summary>
        /// <param name="atList"></param>
        /// <returns></returns>
        public static string At(params long[] atList)
        {
            if (atList == null || atList.Length < 1)
            {
                return "";
            }
            return $"[ATUSER({string.Join(",",atList)})]";
        }
    }
}
