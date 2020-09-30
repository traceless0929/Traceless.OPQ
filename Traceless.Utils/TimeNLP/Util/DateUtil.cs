using System;
using System.Collections.Generic;

/// <summary> Copyright (c) 2016 21CN.COM . All rights reserved.<br> /// Description: calendar<br>
/// /// Modified log:<br>
/// ------------------------------------------------------<br> Ver. Date Author Description<br>
/// ------------------------------------------------------<br> 1.0 2016年3月8日 kexm created.<br> </summary>
namespace Traceless.Utils.TimeNLP.Util
{
    /// <summary>
    /// <para>日程项目的时间工具类，继承公司的公共时间工具类</para>
    /// <para>/// @author <a href="mailto:kexm@corp.21cn.com">kexm</a> @since 2016年3月8日</para>
    /// </summary>
    public class DateUtil : CommonDateUtil
    {
        /// <summary>
        /// 是否是今天
        /// </summary>
        /// <param name="date">@return</param>
        public static bool isToday(DateTime date)
        {
            return isTheDay(date, DateTime.Now);
        }

        /// <summary>
        /// 是否是指定日期
        /// </summary>
        /// <param name="date"></param>
        /// <param name="day">@return</param>
        public static bool isTheDay(DateTime date, DateTime day)
        {
            return date.Ticks >= dayBegin(day).Ticks && date.Ticks <= dayEnd(day).Ticks;
        }

        /// <summary>
        /// 对时间中的分钟向上取整
        /// </summary>
        /// <param name="date"></param>
        /// <param name="round">取整的值 @return</param>
        public static DateTime roundMin(DateTime date, int round)
        {
            if (round > 60 || round < 0)
            {
                round = 0;
            }
            DateTime c = new DateTime();
            c = new DateTime(date.Ticks);
            int min = c.Minute;
            if ((min % round) >= (round / 2))
            {
                min = round * (min / (round + 1));
            }
            else
            {
                min = round * (min / round);
            }

            return new DateTime(date.Year, date.Month, date.Day, date.Hour, min, 0);
        }

        /// <summary>
        /// 获得指定时间那天的某个小时（24小时制）的整点时间
        /// </summary>
        /// <param name="date"></param>
        /// <param name="hourIn24">@return</param>
        public static DateTime getSpecificHourInTheDay(DateTime date, int hourIn24)
        {
            DateTime c = new DateTime(date.Year, date.Month, date.Day, hourIn24, 0, 0);
            return c;
        }

        /// <summary>
        /// 获取指定时间的那天 00:00:00.000 的时间
        /// </summary>
        /// <param name="date">@return</param>
        public static DateTime dayBegin(DateTime date)
        {
            return getSpecificHourInTheDay(date, 0);
        }

        /// <summary>
        /// 获取指定时间的那天 23:59:59.999 的时间
        /// </summary>
        /// <param name="date">@return</param>
        public static DateTime dayEnd(DateTime date)
        {
            DateTime c = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            return c;
        }

        /// <summary>
        /// 默认时间格式化
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format">@return</param>
        public static string formatDateDefault(DateTime date)
        {
            return DateUtil.formatDate(date, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 检测日期格式字符串是否符合format
        /// <para>主要逻辑为先把字符串parse为该format的Date对象，再将Date对象按format转换为string。如果此string与初始字符串一致，则日期符合format。</para>
        /// <para>
        /// 之所以用来回双重逻辑校验，是因为假如把一个非法字符串parse为某format的Date对象是不一定会报错的。 比如 2015-06-29
        /// 13:12:121，明显不符合yyyy-MM-dd HH:mm:ss，但是可以正常parse成Date对象，但时间变为了2015-06-29
        /// 13:14:01。增加多一重校验则可检测出这个问题。 ///
        /// </para>
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <param name="format">日期格式</param>
        /// <returns>boolean</returns>
        public static bool checkDateFormatAndValite(string strDateTime, string format)
        {
            if (string.ReferenceEquals(strDateTime, null) || strDateTime.Length == 0)
            {
                return false;
            }
            try
            {
                DateTime ndate = DateTime.ParseExact(strDateTime, format, System.Globalization.CultureInfo.CurrentCulture);
                string str = ndate.ToString(format);
                if (str.Equals(strDateTime))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.Write(e.StackTrace);
                return false;
            }
        }

        //日期格式为:年 月 日 ；如：2016年04月06日
        public const string FORMAT_CALENDAR_DATE = "yyyy\u5E74MM\u6708dd\u65E5E";

        //时间格式 为：小时：分 ;如：12:30
        public const string FORMAT_CALENDAR_TIME = "HH:mm";
    }
}