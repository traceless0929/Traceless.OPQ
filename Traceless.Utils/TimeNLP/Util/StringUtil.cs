/// <summary> Copyright (c) 2016 21CN.COM . All rights reserved.<br> /// Description:
/// calendarCommon<br> /// Modified log:<br>
/// ------------------------------------------------------<br> Ver. Date Author Description<br>
/// ------------------------------------------------------<br> 1.0 2016年4月25日 kexm created.<br> </summary>
namespace Traceless.Utils.TimeNLP.Util
{
    /// <summary>
    /// <para>字符串工具类</para>
    /// <para>@author <a href="mailto:kexm@corp.21cn.com">kexm</a> @version @since 2016年4月25日 ///</para>
    /// </summary>
    public class StringUtil
    {
        /// <summary>
        /// 字符串是否为空
        /// </summary>
        /// <param name="str">@return</param>
        public static bool isEmpty(string str)
        {
            return ((string.ReferenceEquals(str, null)) || (str.Trim().Length == 0));
        }
    }
}