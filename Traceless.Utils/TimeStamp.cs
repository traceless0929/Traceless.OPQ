using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.Utils
{
    public class TimeStamp
    {
        /// <summary>
        /// 日期转换为时间戳（时间戳单位秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp(DateTime time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - Jan1st1970).TotalMilliseconds;
        }
    }
}