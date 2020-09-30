using System;
using System.Globalization;

/// <summary> Copyright (c) 2016 21CN.COM . All rights reserved.<br> /// Description:
/// calendarCommon<br> /// Modified log:<br>
/// ------------------------------------------------------<br> Ver. Date Author Description<br>
/// ------------------------------------------------------<br> 1.0 2016年4月25日 kexm created.<br> </summary>
namespace Traceless.Utils.TimeNLP.Util
{
    /// <summary>
    /// <para>日期工具类（来自公司公共项目）</para>
    /// <para>@author <a href="mailto:kexm@corp.21cn.com">kexm</a> @version @since 2016年4月25日 ///</para>
    /// </summary>
    public class CommonDateUtil
    {
        private static string defaultDatePattern = "yyyy-MM-dd";
        public const long ONE_MINUTE_MILLISECOND = 60000L;
        public const long ONE_HOUR_MILLISECOND = 3600000L;
        public const long ONE_DAY_MILLISECOND = 86400000L;
        public const long ONE_WEEK_MILLISECOND = 604800000L;
        public const long ONE_MONTH_MILLISECOND = 2592000000L;
        public const long ONE_YEAR_MILLISECOND = 31536000000L;
        private static readonly string[] SMART_DATE_FORMATS = new string[] { "yyyy-MM-dd HH:mm:ss", "yyyy.MM.dd HH:mm:ss", "yyyy-MM-dd HH:mm", "yyyy.MM.dd HH:mm", "yyyyMMddHHmmss", "yyyyMMddHHmm", "yyyy-MM-dd", "yyyy.MM.dd", "yyyyMMdd" };

        public static readonly string[] zodiacArray = new string[] { "猴", "鸡", "狗", "猪", "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊" };

        public static readonly string[] constellationArray = new string[] { "水瓶座", "双鱼座", "牡羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "魔羯座" };

        private static readonly int[] constellationEdgeDay = new int[] { 20, 19, 21, 21, 21, 22, 23, 23, 23, 23, 22, 22 };

        public static string DatePattern
        {
            get
            {
                return defaultDatePattern;
            }
        }

        public static int getYear(DateTime date)
        {
            return getCalendar(date).Year;
        }

        public static int getMonth(DateTime date)
        {
            return getCalendar(date).Month;
        }

        public static int getDay(DateTime date)
        {
            return getCalendar(date).Day;
        }

        public static int getWeek(DateTime date)
        {
            return Convert.ToInt32(getCalendar(date).DayOfWeek);
        }

        public static int getWeekOfFirstDayOfMonth(DateTime date)
        {
            return getWeek(getFirstDayOfMonth(date));
        }

        public static int getWeekOfLastDayOfMonth(DateTime date)
        {
            return getWeek(getLastDayOfMonth(date));
        }

        public static DateTime parseDate(string strDate, string format)
        {
            try
            {
                return DateTime.ParseExact(strDate, format, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
            }
            return DateTime.MinValue;
        }

        public static DateTime parseDateSmart(string strDate)
        {
            if (StringUtil.isEmpty(strDate))
            {
                return DateTime.MinValue;
            }
            foreach (string fmt in SMART_DATE_FORMATS)
            {
                DateTime d = parseDate(strDate, fmt);
                if (d != null)
                {
                    string s = formatDate(d, fmt);
                    if (strDate.Equals(s))
                    {
                        return d;
                    }
                }
            }
            try
            {
                long time = long.Parse(strDate);
                return new DateTime(time);
            }
            catch (Exception)
            {
            }
            return DateTime.MinValue;
        }

        public static DateTime parseDate(string strDate)
        {
            return parseDate(strDate, DatePattern);
        }

        public static bool isLeapYear(int year)
        {
            if (year / 4 * 4 != year)
            {
                return false;
            }
            if (year / 100 * 100 != year)
            {
                return true;
            }

            return (year / 400 * 400 == year);
        }

        public static bool isWeekend(DateTime date)
        {
            DateTime c = new DateTime();
            if (date != null)
            {
                c = date;
            }
            int weekDay = Convert.ToInt32(c.DayOfWeek);
            return ((weekDay == 1) || (weekDay == 7));
        }

        public static bool Weekend
        {
            get
            {
                return isWeekend(DateTime.MinValue);
            }
        }

        public static string CurrentTime
        {
            get
            {
                return formatDate(DateTime.Now);
            }
        }

        public static string getCurrentTime(string format)
        {
            return formatDate(DateTime.Now, format);
        }

        public static string formatDate(DateTime date, string format)
        {
            if (date == null)
            {
                date = DateTime.Now;
            }
            if (string.ReferenceEquals(format, null))
            {
                format = DatePattern;
            }
            return date.ToString(format);
        }

        public static string formatDate(DateTime date)
        {
            long offset = DateTimeHelper.CurrentUnixTimeMillis() - date.Ticks;
            string pos = "前";
            if (offset < 0L)
            {
                pos = "后";
                offset = -offset;
            }
            if (offset >= 31536000000L)
            {
                return formatDate(date, DatePattern);
            }
            if (offset >= 5184000000L)
            {
                return ((offset + 1296000000L) / 2592000000L) + "个月" + pos;
            }
            if (offset > 604800000L)
            {
                return ((offset + 302400000L) / 604800000L) + "周" + pos;
            }
            if (offset > 86400000L)
            {
                return ((offset + 43200000L) / 86400000L) + "天" + pos;
            }
            if (offset > 3600000L)
            {
                return ((offset + 1800000L) / 3600000L) + "小时" + pos;
            }
            if (offset > 60000L)
            {
                return ((offset + 30000L) / 60000L) + "分钟" + pos;
            }
            return (offset / 1000L) + "秒" + pos;
        }

        public static DateTime getCleanDay(DateTime day)
        {
            return getCleanDay(getCalendar(day));
        }

        public static DateTime getCalendar(DateTime day)
        {
            DateTime c = new DateTime(day.Ticks);
            return c;
        }

        public static DateTime makeDate(int year, int month, int day)
        {
            DateTime c = new DateTime(year, month, day);
            return c;
        }

        /// <summary>
        /// 得到本周周一
        /// </summary>
        /// <returns>Date</returns>
        public static DateTime getFirstDayOfWeek(DateTime date)
        {
            DateTime c = new DateTime(date.Ticks);
            int day_of_week = Convert.ToInt32(c.DayOfWeek);
            if (day_of_week == 0)
            {
                day_of_week = 7;
            }
            c.AddDays(-day_of_week + 1);
            return c;
        }

        public static DateTime FirstDayOfWeek
        {
            get
            {
                return getFirstDayOfWeek(DateTime.Now);
            }
        }

        public static DateTime getFirstDayOfMonth(DateTime date)
        {
            return date.AddDays(1 - date.Day);
        }

        public static DateTime FirstDayOfMonth
        {
            get
            {
                return getFirstDayOfMonth(DateTime.Now);
            }
        }

        public static DateTime LastDayOfMonth
        {
            get
            {
                return getLastDayOfMonth(DateTime.Now);
            }
        }

        public static DateTime getLastDayOfMonth(DateTime date)
        {
            return date.AddDays(1 - date.Day).AddMonths(1).AddDays(-1);
        }

        public static DateTime getFirstDayOfSeason(DateTime date)
        {
            DateTime d = getFirstDayOfMonth(date);
            int delta = getMonth(d) % 3;
            if (delta > 0)
            {
                d = getDateAfterMonths(d, -delta);
            }
            return d;
        }

        public static DateTime FirstDayOfSeason
        {
            get
            {
                return getFirstDayOfSeason(DateTime.Now);
            }
        }

        public static DateTime getFirstDayOfYear(DateTime date)
        {
            return makeDate(getYear(date), 1, 1);
        }

        public static DateTime FirstDayOfYear
        {
            get
            {
                return getFirstDayOfYear(DateTime.Now);
            }
        }

        public static DateTime getDateAfterWeeks(DateTime start, int weeks)
        {
            return getDateAfterMs(start, weeks * 604800000L);
        }

        public static DateTime getDateAfterMonths(DateTime start, int months)
        {
            return start.AddMonths(months);
        }

        public static DateTime getDateAfterYears(DateTime start, int years)
        {
            return start.AddYears(years);
        }

        public static DateTime getDateAfterDays(DateTime start, int days)
        {
            return getDateAfterMs(start, days * 86400000L);
        }

        public static DateTime getDateAfterMs(DateTime start, long ms)
        {
            return new DateTime(start.Ticks + ms);
        }

        public static long getPeriodNum(DateTime start, DateTime end, long msPeriod)
        {
            return (getIntervalMs(start, end) / msPeriod);
        }

        public static long getIntervalMs(DateTime start, DateTime end)
        {
            return (end.Ticks - start.Ticks);
        }

        public static int getIntervalDays(DateTime start, DateTime end)
        {
            return (int)getPeriodNum(start, end, 86400000L);
        }

        public static int getIntervalWeeks(DateTime start, DateTime end)
        {
            return (int)getPeriodNum(start, end, 604800000L);
        }

        public static bool before(DateTime @base, DateTime date)
        {
            return ((date < @base) || (date.Equals(@base)));
        }

        public static bool after(DateTime @base, DateTime date)
        {
            return ((date > @base) || (date.Equals(@base)));
        }

        public static DateTime max(DateTime date1, DateTime date2)
        {
            if (date1.Ticks > date2.Ticks)
            {
                return date1;
            }
            return date2;
        }

        public static DateTime min(DateTime date1, DateTime date2)
        {
            if (date1.Ticks < date2.Ticks)
            {
                return date1;
            }
            return date2;
        }

        public static bool inPeriod(DateTime start, DateTime end, DateTime date)
        {
            return ((((end > date) || (end.Equals(date)))) && (((start < date) || (start.Equals(date)))));
        }

        public static string date2Zodica(DateTime time)
        {
            DateTime c = new DateTime();
            c = new DateTime(time.Ticks);
            return year2Zodica(c.Year);
        }

        public static string year2Zodica(int year)
        {
            return zodiacArray[(year % 12)];
        }

        public static string date2Constellation(DateTime time)
        {
            DateTime c = new DateTime();
            c = new DateTime(time.Ticks);
            int month = c.Month;
            int day = c.Day;
            if (day < constellationEdgeDay[month])
            {
                --month;
            }
            if (month >= 0)
            {
                return constellationArray[month];
            }

            return constellationArray[11];
        }

        public static void TestMain(string[] args)
        {
            Console.WriteLine(year2Zodica(1973));
            Console.WriteLine(date2Zodica(DateTime.Now));
            Console.WriteLine(date2Constellation(makeDate(1973, 5, 12)));
            Console.WriteLine(new DateTime() == new DateTime());
            Console.WriteLine(getCleanDay(DateTime.Now));
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(FirstDayOfMonth);
            Console.WriteLine(getLastDayOfMonth(makeDate(1996, 2, 1)));

            Console.WriteLine(formatDate(makeDate(2009, 5, 1)));
            Console.WriteLine(formatDate(makeDate(2010, 5, 1)));
            Console.WriteLine(formatDate(makeDate(2010, 12, 21)));
            Console.WriteLine(before(makeDate(2009, 5, 1), DateTime.Now));
            Console.WriteLine(after(makeDate(2009, 5, 1), DateTime.Now));
            Console.WriteLine(inPeriod(makeDate(2009, 11, 24), makeDate(2009, 11, 30), makeDate(2009, 11, 25)));
        }
    }
}