/// <summary> Copyright (c) 2016 21CN.COM . All rights reserved.<br> /// Description: fudannlp<br>
/// /// Modified log:<br>
/// ------------------------------------------------------<br> Ver. Date Author Description<br>
/// ------------------------------------------------------<br> 1.0 2016年5月4日 kexm created.<br> </summary>
namespace Traceless.Utils.TimeNLP.NLP
{
    /// <summary>
    /// <para>时间表达式单元规范化的内部类 /// 时间表达式单元规范化对应的内部类, 对应时间表达式规范化的每个字段， 六个字段分别是：年-月-日-时-分-秒， 每个字段初始化为-1</para>
    /// <para>@author <a href="mailto:kexm@corp.21cn.com">kexm</a> @version @since 2016年5月4日 ///</para>
    /// </summary>
    public class TimePoint
    {
        internal int[] tunit = new int[] { -1, -1, -1, -1, -1, -1 };
    }
}