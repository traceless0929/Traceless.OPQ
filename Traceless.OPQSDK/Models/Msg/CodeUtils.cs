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
        /// <param name="atList">传一个-1表示At全体成员</param>
        /// <returns></returns>
        public static string At(params long[] atList)
        {
            if (atList == null || atList.Length < 1)
            {
                return "";
            }
            if(atList.Length==1&&atList[0]==-1){
                return $"[ATALL()]";
            }
            return $"[ATUSER({string.Join(",",atList)})]";
        }

        /// <summary>
        /// 获取昵称
        /// </summary>
        /// <param name="qq">QQ号</param>
        /// <returns></returns>
        public static string UserNick(long qq)
        {
            return $"[GETUSERNICK({qq})]";
        }
    }
}
