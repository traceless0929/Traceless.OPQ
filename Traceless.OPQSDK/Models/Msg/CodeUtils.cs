using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Msg
{
    public class CodeUtils
    {
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
