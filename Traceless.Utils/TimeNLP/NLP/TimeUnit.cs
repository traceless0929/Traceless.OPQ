using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Traceless.Utils.TimeNLP.Enums;

namespace Traceless.Utils.TimeNLP.NLP
{
    /// <summary>
    /// <para>时间语句分析</para>
    /// <para>/// @author <a href="mailto:kexm@corp.21cn.com">kexm</a> @since 2016年5月4日</para>
    /// </summary>
    public class TimeUnit
    {
        //有需要可使用
        //private static final Logger LOGGER = LoggerFactory.getLogger(TimeUnit.class);
        /// <summary>
        /// 目标字符串
        /// </summary>
        public string Time_Expression = null;

        public string Time_Norm = "";
        public int[] time_full;
        public int[] time_origin;
        private DateTime time;
        private bool? isAllDayTime = true;
        private bool isFirstTimeSolveContext = true;

        internal TimeNormalizer normalizer = null;
        public TimePoint _tp = new TimePoint();
        public TimePoint _tp_origin = new TimePoint();

        /// <summary>
        /// 时间表达式单元构造方法 该方法作为时间表达式单元的入口，将时间表达式字符串传入
        /// </summary>
        /// <param name="exp_time">时间表达式字符串</param>
        /// <param name="n"></param>

        public TimeUnit(string exp_time, TimeNormalizer n)
        {
            Time_Expression = exp_time;
            normalizer = n;
            Time_Normalization();
        }

        /// <summary>
        /// 时间表达式单元构造方法 该方法作为时间表达式单元的入口，将时间表达式字符串传入
        /// </summary>
        /// <param name="exp_time">时间表达式字符串</param>
        /// <param name="n"></param>
        /// <param name="contextTp">上下文时间</param>

        public TimeUnit(string exp_time, TimeNormalizer n, TimePoint contextTp)
        {
            Time_Expression = exp_time;
            normalizer = n;
            _tp_origin = contextTp;
            Time_Normalization();
        }

        /// <summary>
        /// return the accurate time object
        /// </summary>
        public virtual DateTime Time
        {
            get
            {
                return time;
            }
        }

        /// <summary>
        /// 年-规范化方法
        /// <para>该方法识别时间表达式单元的年字段</para>
        /// </summary>
        public virtual void norm_setyear()
        {
            /// <summary>
            ///假如只有两位数来表示年份 </summary>
            string rule = "[0-9]{2}(?=年)";
            Regex pattern = new Regex(rule);
            Match match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                _tp.tunit[0] = int.Parse(match.Value);
                if (_tp.tunit[0] >= 0 && _tp.tunit[0] < 100)
                {
                    if (_tp.tunit[0] < 30) //*30以下表示2000年以后的年份
                    {
                        _tp.tunit[0] += 2000;
                    }
                    else //*否则表示1900年以后的年份
                    {
                        _tp.tunit[0] += 1900;
                    }
                }
            }
            /// <summary>
            ///不仅局限于支持1XXX年和2XXX年的识别，可识别三位数和四位数表示的年份 </summary>
            rule = "[0-9]?[0-9]{3}(?=年)";

            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                _tp.tunit[0] = int.Parse(match.Value);
            }
        }

        /// <summary>
        /// 月-规范化方法
        /// <para>该方法识别时间表达式单元的月字段</para>
        /// </summary>
        public virtual void norm_setmonth()
        {
            string rule = "((10)|(11)|(12)|([1-9]))(?=月)";
            Regex pattern = new Regex(rule);
            Match match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                _tp.tunit[1] = int.Parse(match.Value);

                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(1);
            }
        }

        /// <summary>
        /// 月-日 兼容模糊写法
        /// <para>该方法识别时间表达式单元的月、日字段</para>
        /// <para>add by kexm</para>
        /// </summary>
        public virtual void norm_setmonth_fuzzyday()
        {
            string rule = "((10)|(11)|(12)|([1-9]))(月|\\.|\\-)([0-3][0-9]|[1-9])";
            Regex pattern = new Regex(rule);
            Match match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                string matchStr = match.Value;
                Regex p = new Regex("(月|\\.|\\-)");
                Match m = p.Match(matchStr);
                if (m.Success)
                {
                    int splitIndex = m.Index;
                    string month = matchStr.Substring(0, splitIndex);
                    string date = matchStr.Substring(splitIndex + 1);

                    _tp.tunit[1] = int.Parse(month);
                    _tp.tunit[2] = int.Parse(date);

                    /// <summary>
                    ///处理倾向于未来时间的情况  @author kexm </summary>
                    preferFuture(1);
                }
            }
        }

        /// <summary>
        /// 日-规范化方法
        /// <para>该方法识别时间表达式单元的日字段</para>
        /// </summary>
        public virtual void norm_setday()
        {
            string rule = "((?<!\\d))([0-3][0-9]|[1-9])(?=(日|号))";
            Regex pattern = new Regex(rule);
            Match match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                _tp.tunit[2] = int.Parse(match.Value);

                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(2);
            }
        }

        /// <summary>
        /// 时-规范化方法
        /// <para>该方法识别时间表达式单元的时字段</para>
        /// </summary>
        public virtual void norm_sethour()
        {
            string rule = "(?<!(周|星期))([0-2]?[0-9])(?=(点|时))";

            Regex pattern = new Regex(rule);
            Match match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                _tp.tunit[3] = int.Parse(match.Value);
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(3);
                isAllDayTime = false;
            }
            /*
             * 对关键字：早（包含早上/早晨/早间），上午，中午,午间,下午,午后,晚上,傍晚,晚间,晚,pm,PM的正确时间计算
             * 规约：
             * 1.中午/午间0-10点视为12-22点
             * 2.下午/午后0-11点视为12-23点
             * 3.晚上/傍晚/晚间/晚1-11点视为13-23点，12点视为0点
             * 4.0-11点pm/PM视为12-23点
             *
             * add by kexm
             */
            rule = "凌晨";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                if (_tp.tunit[3] == -1) //*增加对没有明确时间点，只写了"凌晨"这种情况的处理 @author kexm
                {
                    _tp.tunit[3] = RangeTimeEnum.day_break.HourTime;
                }
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(3);
                isAllDayTime = false;
            }

            rule = "早上|早晨|早间|晨间|今早|明早";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                if (_tp.tunit[3] == -1) //*增加对没有明确时间点，只写了"早上/早晨/早间"这种情况的处理 @author kexm
                {
                    _tp.tunit[3] = RangeTimeEnum.early_morning.HourTime;
                }
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(3);
                isAllDayTime = false;
            }

            rule = "上午";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                if (_tp.tunit[3] == -1) //*增加对没有明确时间点，只写了"上午"这种情况的处理 @author kexm
                {
                    _tp.tunit[3] = RangeTimeEnum.morning.HourTime;
                }
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(3);
                isAllDayTime = false;
            }

            rule = "(中午)|(午间)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                if (_tp.tunit[3] >= 0 && _tp.tunit[3] <= 10)
                {
                    _tp.tunit[3] += 12;
                }
                if (_tp.tunit[3] == -1) //*增加对没有明确时间点，只写了"中午/午间"这种情况的处理 @author kexm
                {
                    _tp.tunit[3] = RangeTimeEnum.noon.HourTime;
                }
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(3);
                isAllDayTime = false;
            }

            rule = "(下午)|(午后)|(pm)|(PM)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                if (_tp.tunit[3] >= 0 && _tp.tunit[3] <= 11)
                {
                    _tp.tunit[3] += 12;
                }
                if (_tp.tunit[3] == -1) //*增加对没有明确时间点，只写了"下午|午后"这种情况的处理  @author kexm
                {
                    _tp.tunit[3] = RangeTimeEnum.afternoon.HourTime;
                }
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(3);
                isAllDayTime = false;
            }

            rule = "晚上|夜间|夜里|今晚|明晚";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                if (_tp.tunit[3] >= 1 && _tp.tunit[3] <= 11)
                {
                    _tp.tunit[3] += 12;
                }
                else if (_tp.tunit[3] == 12)
                {
                    _tp.tunit[3] = 0;
                }
                else if (_tp.tunit[3] == -1)
                {
                    _tp.tunit[3] = RangeTimeEnum.night.HourTime;
                }

                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(3);
                isAllDayTime = false;
            }
        }

        /// <summary>
        /// 分-规范化方法
        /// <para>该方法识别时间表达式单元的分字段</para>
        /// </summary>
        public virtual void norm_setminute()
        {
            string rule = "([0-5]?[0-9](?=分(?!钟)))|((?<=((?<!小)[点时]))[0-5]?[0-9](?!刻))";

            Regex pattern = new Regex(rule);
            Match match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                if (!match.Value.Equals(""))
                {
                    _tp.tunit[4] = int.Parse(match.Value);
                    /// <summary>
                    ///处理倾向于未来时间的情况  @author kexm </summary>
                    preferFuture(4);
                    isAllDayTime = false;
                }
            }
            /// <summary>
            /// 加对一刻，半，3刻的正确识别（1刻为15分，半为30分，3刻为45分）
            /// </summary>
            rule = "(?<=[点时])[1一]刻(?!钟)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                _tp.tunit[4] = 15;
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(4);
                isAllDayTime = false;
            }

            rule = "(?<=[点时])半";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                _tp.tunit[4] = 30;
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(4);
                isAllDayTime = false;
            }

            rule = "(?<=[点时])[3三]刻(?!钟)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                _tp.tunit[4] = 45;
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(4);
                isAllDayTime = false;
            }
        }

        /// <summary>
        /// 秒-规范化方法
        /// <para>该方法识别时间表达式单元的秒字段</para>
        /// </summary>
        public virtual void norm_setsecond()
        {
            /*
             * 添加了省略"分"说法的时间
             * 如17点15分32
             * modified by 曹零
             */
            string rule = "([0-5]?[0-9](?=秒))|((?<=分)[0-5]?[0-9])";

            Regex pattern = new Regex(rule);
            Match match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                _tp.tunit[5] = int.Parse(match.Value);
                isAllDayTime = false;
            }
        }

        /// <summary>
        /// 特殊形式的规范化方法
        /// <para>该方法识别特殊形式的时间表达式单元的各个字段</para>
        /// </summary>
        public virtual void norm_setTotal()
        {
            string rule;
            Regex pattern;
            Match match;
            string[] tmp_parser;
            string tmp_target;

            rule = "(?<!(周|星期))([0-2]?[0-9]):[0-5]?[0-9]:[0-5]?[0-9]";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                tmp_parser = new string[3];
                tmp_target = match.Value;
                tmp_parser = tmp_target.Split(":", true);
                _tp.tunit[3] = int.Parse(tmp_parser[0]);
                _tp.tunit[4] = int.Parse(tmp_parser[1]);
                _tp.tunit[5] = int.Parse(tmp_parser[2]);
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(3);
                isAllDayTime = false;
            }
            else
            {
                rule = "(?<!(周|星期))([0-2]?[0-9]):[0-5]?[0-9]";
                pattern = new Regex(rule);
                match = pattern.Match(Time_Expression);
                if (match.Success)
                {
                    tmp_parser = new string[2];
                    tmp_target = match.Value;
                    tmp_parser = tmp_target.Split(":", true);
                    _tp.tunit[3] = int.Parse(tmp_parser[0]);
                    _tp.tunit[4] = int.Parse(tmp_parser[1]);
                    /// <summary>
                    ///处理倾向于未来时间的情况  @author kexm </summary>
                    preferFuture(3);
                    isAllDayTime = false;
                }
            }
            /*
             * 增加了:固定形式时间表达式的
             * 中午,午间,下午,午后,晚上,傍晚,晚间,晚,pm,PM
             * 的正确时间计算，规约同上
             */
            rule = "(中午)|(午间)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                if (_tp.tunit[3] >= 0 && _tp.tunit[3] <= 10)
                {
                    _tp.tunit[3] += 12;
                }
                if (_tp.tunit[3] == -1) //*增加对没有明确时间点，只写了"中午/午间"这种情况的处理 @author kexm
                {
                    _tp.tunit[3] = RangeTimeEnum.noon.HourTime;
                }
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(3);
                isAllDayTime = false;
            }

            rule = "(下午)|(午后)|(pm)|(PM)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                if (_tp.tunit[3] >= 0 && _tp.tunit[3] <= 11)
                {
                    _tp.tunit[3] += 12;
                }
                if (_tp.tunit[3] == -1) //*增加对没有明确时间点，只写了"中午/午间"这种情况的处理 @author kexm
                {
                    _tp.tunit[3] = RangeTimeEnum.afternoon.HourTime;
                }
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(3);
                isAllDayTime = false;
            }

            rule = "晚";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                if (_tp.tunit[3] >= 1 && _tp.tunit[3] <= 11)
                {
                    _tp.tunit[3] += 12;
                }
                else if (_tp.tunit[3] == 12)
                {
                    _tp.tunit[3] = 0;
                }
                if (_tp.tunit[3] == -1) //*增加对没有明确时间点，只写了"中午/午间"这种情况的处理 @author kexm
                {
                    _tp.tunit[3] = RangeTimeEnum.night.HourTime;
                }
                /// <summary>
                ///处理倾向于未来时间的情况  @author kexm </summary>
                preferFuture(3);
                isAllDayTime = false;
            }

            rule = "[0-9]?[0-9]?[0-9]{2}-((10)|(11)|(12)|([1-9]))-((?<!\\d))([0-3][0-9]|[1-9])";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                tmp_parser = new string[3];
                tmp_target = match.Value;
                tmp_parser = tmp_target.Split("-", true);
                _tp.tunit[0] = int.Parse(tmp_parser[0]);
                _tp.tunit[1] = int.Parse(tmp_parser[1]);
                _tp.tunit[2] = int.Parse(tmp_parser[2]);
            }

            rule = "((10)|(11)|(12)|([1-9]))/((?<!\\d))([0-3][0-9]|[1-9])/[0-9]?[0-9]?[0-9]{2}";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                tmp_parser = new string[3];
                tmp_target = match.Value;
                tmp_parser = tmp_target.Split("/", true);
                _tp.tunit[1] = int.Parse(tmp_parser[0]);
                _tp.tunit[2] = int.Parse(tmp_parser[1]);
                _tp.tunit[0] = int.Parse(tmp_parser[2]);
            }

            /*
             * 增加了:固定形式时间表达式 年.月.日 的正确识别
             * add by 曹零
             */
            rule = "[0-9]?[0-9]?[0-9]{2}\\.((10)|(11)|(12)|([1-9]))\\.((?<!\\d))([0-3][0-9]|[1-9])";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                tmp_parser = new string[3];
                tmp_target = match.Value;
                tmp_parser = tmp_target.Split("\\.", true);
                _tp.tunit[0] = int.Parse(tmp_parser[0]);
                _tp.tunit[1] = int.Parse(tmp_parser[1]);
                _tp.tunit[2] = int.Parse(tmp_parser[2]);
            }
        }

        /// <summary>
        /// 设置以上文时间为基准的时间偏移计算
        /// </summary>
        public virtual void norm_setBaseRelated()
        {
            string[] time_grid = new string[6];
            time_grid = normalizer.TimeBase.Split('-');
            int[] ini = new int[6];
            for (int i = 0; i < 6; i++)
            {
                ini[i] = int.Parse(time_grid[i]);
            }

            DateTime calendar = new DateTime();
            calendar = new DateTime(ini[0], ini[1], ini[2], ini[3], ini[4], ini[5]);

            bool[] flag = new bool[] { false, false, false }; //观察时间表达式是否因当前相关时间表达式而改变时间

            string rule = "\\d+(?=天[以之]?前)";
            Regex pattern = new Regex(rule);
            Match match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                int day = int.Parse(match.Value);
                calendar.AddDays(-day);
            }

            rule = "\\d+(?=天[以之]?后)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                int day = int.Parse(match.Value);
                calendar.AddDays(day);
            }

            rule = "\\d+(?=(个)?月[以之]?前)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[1] = true;
                int month = int.Parse(match.Value);
                calendar.AddMonths(-month);
            }

            rule = "\\d+(?=(个)?月[以之]?后)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[1] = true;
                int month = int.Parse(match.Value);
                calendar.AddMonths(month);
            }

            rule = "\\d+(?=年[以之]?前)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[0] = true;
                int year = int.Parse(match.Value);
                calendar.AddYears(-year);
            }

            rule = "\\d+(?=年[以之]?后)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[0] = true;
                int year = int.Parse(match.Value);
                calendar.AddYears(year);
            }

            string s = calendar.ToString("yyyy-MM-dd-HH-mm-ss");
            string[] time_fin = s.Split("-", true);
            if (flag[0] || flag[1] || flag[2])
            {
                _tp.tunit[0] = int.Parse(time_fin[0]);
            }
            if (flag[1] || flag[2])
            {
                _tp.tunit[1] = int.Parse(time_fin[1]);
            }
            if (flag[2])
            {
                _tp.tunit[2] = int.Parse(time_fin[2]);
            }
        }

        /// <summary>
        /// 设置当前时间相关的时间表达式
        /// </summary>
        public virtual void norm_setCurRelated()
        {
            string[] time_grid = new string[6];
            time_grid = normalizer.OldTimeBase.Split('-');
            int[] ini = new int[6];
            for (int i = 0; i < 6; i++)
            {
                ini[i] = int.Parse(time_grid[i]);
            }

            DateTime calendar = new DateTime();
            calendar = new DateTime(ini[0], ini[1], ini[2], ini[3], ini[4], ini[5]);

            bool[] flag = new bool[] { false, false, false }; //观察时间表达式是否因当前相关时间表达式而改变时间

            string rule = "前年";
            Regex pattern = new Regex(rule);
            Match match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[0] = true;
                calendar.AddYears(-2);
            }

            rule = "去年";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[0] = true;
                calendar.AddYears(-1);
            }

            rule = "今年";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[0] = true;
                calendar.AddYears(0);
            }

            rule = "明年";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[0] = true;
                calendar.AddYears(1);
            }

            rule = "后年";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[0] = true;
                calendar.AddYears(2);
            }

            rule = "上(个)?月";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[1] = true;
                calendar.AddMonths(-1);
            }

            rule = "(本|这个)月";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[1] = true;
                calendar.AddMonths(0);
            }

            rule = "下(个)?月";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[1] = true;
                calendar.AddMonths(1);
            }

            rule = "大前天";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                calendar.AddDays(-3);
            }

            rule = "(?<!大)前天";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                calendar.AddDays(-2);
            }

            rule = "昨";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                calendar.AddDays(-1);
            }

            rule = "今(?!年)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                calendar.AddDays(0);
            }

            rule = "明(?!年)";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                calendar.AddDays(1);
            }

            rule = "(?<!大)后天";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                calendar.AddDays(2);
            }

            rule = "大后天";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                calendar.AddDays(3);
            }

            rule = "(?<=(上上(周|星期)))[1-7]?";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                int week;
                try
                {
                    week = int.Parse(match.Value);
                }
                catch (System.FormatException)
                {
                    week = 1;
                }
                if (week == 7)
                {
                    //week = 0;
                }
                else
                {
                    //week++;
                }
                calendar = calendar.AddDays(-14);
                int dayOfWeek = Convert.ToInt32(calendar.DayOfWeek); dayOfWeek = (dayOfWeek == 0 ? 7 : dayOfWeek);
                int dayOfDelta = week - dayOfWeek;
                calendar = calendar.AddDays(dayOfDelta);
            }

            rule = "(?<=((?<!上)上(周|星期)))[1-7]?";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                int week;
                try
                {
                    week = int.Parse(match.Value);
                }
                catch (System.FormatException)
                {
                    week = 1;
                }
                if (week == 7)
                {
                    //week = 0;
                }
                else
                {
                    //week++;
                }
                calendar = calendar.AddDays(-7);
                int dayOfWeek = Convert.ToInt32(calendar.DayOfWeek); dayOfWeek = (dayOfWeek == 0 ? 7 : dayOfWeek);
                int dayOfDelta = week - dayOfWeek;
                calendar = calendar.AddDays(dayOfDelta);
            }

            rule = "(?<=((?<!下)下(周|星期)))[1-7]?";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                int week;
                try
                {
                    week = int.Parse(match.Value);
                }
                catch (System.FormatException)
                {
                    week = 1;
                }
                if (week == 7)
                {
                    //week = 0;
                }
                else
                {
                    //week++;
                }
                calendar = calendar.AddDays(7);
                int dayOfWeek = Convert.ToInt32(calendar.DayOfWeek); dayOfWeek = (dayOfWeek == 0 ? 7 : dayOfWeek);
                int dayOfDelta = week - dayOfWeek;
                calendar = calendar.AddDays(dayOfDelta);
            }

            rule = "(?<=(下下(周|星期)))[1-7]?";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                int week;
                try
                {
                    week = int.Parse(match.Value);
                }
                catch (System.FormatException)
                {
                    week = 1;
                }
                if (week == 7)
                {
                    //week = 0;
                }
                else
                {
                    //week++;
                }
                calendar = calendar.AddDays(14);
                int dayOfWeek = Convert.ToInt32(calendar.DayOfWeek); dayOfWeek = (dayOfWeek == 0 ? 7 : dayOfWeek);
                int dayOfDelta = week - dayOfWeek;
                calendar = calendar.AddDays(dayOfDelta);
            }

            rule = "(?<=((?<!(上|下))(周|星期)))[1-7]?";
            pattern = new Regex(rule);
            match = pattern.Match(Time_Expression);
            if (match.Success)
            {
                flag[2] = true;
                int week;
                try
                {
                    week = int.Parse(match.Value);
                }
                catch (System.FormatException)
                {
                    week = 1;
                }
                if (week == 7)
                {
                    //week = 0;
                }
                else
                {
                    //week++;
                }
                int dayOfWeek = Convert.ToInt32(calendar.DayOfWeek); dayOfWeek = (dayOfWeek == 0 ? 7 : dayOfWeek);
                int dayOfDelta = week - dayOfWeek;
                calendar = calendar.AddDays(dayOfDelta);
                /// <summary>
                ///处理未来时间倾向 @author kexm </summary>
                preferFutureWeek(week, ref calendar);
            }

            string s = calendar.ToString("yyyy-MM-dd-HH-mm-ss");
            string[] time_fin = s.Split("-", true);
            if (flag[0] || flag[1] || flag[2])
            {
                _tp.tunit[0] = int.Parse(time_fin[0]);
            }
            if (flag[1] || flag[2])
            {
                _tp.tunit[1] = int.Parse(time_fin[1]);
            }
            if (flag[2])
            {
                _tp.tunit[2] = int.Parse(time_fin[2]);
            }
        }

        /// <summary>
        /// 该方法用于更新timeBase使之具有上下文关联性
        /// </summary>
        public virtual void modifyTimeBase()
        {
            string[] time_grid = new string[6];
            time_grid = normalizer.TimeBase.Split('-');

            string s = "";
            if (_tp.tunit[0] != -1)
            {
                s += Convert.ToString(_tp.tunit[0]);
            }
            else
            {
                s += time_grid[0];
            }
            for (int i = 1; i < 6; i++)
            {
                s += "-";
                if (_tp.tunit[i] != -1)
                {
                    s += Convert.ToString(_tp.tunit[i]);
                }
                else
                {
                    s += time_grid[i];
                }
            }
            normalizer.TimeBase = s;
        }

        /// <summary>
        /// 时间表达式规范化的入口
        /// <para>时间表达式识别后，通过此入口进入规范化阶段， 具体识别每个字段的值</para>
        /// </summary>
        public virtual void Time_Normalization()
        {
            norm_setyear();
            norm_setmonth();
            norm_setday();
            norm_setmonth_fuzzyday(); //*add by kexm
            norm_setBaseRelated();
            norm_setCurRelated();
            norm_sethour();
            norm_setminute();
            norm_setsecond();
            norm_setTotal();
            modifyTimeBase();

            _tp.tunit.CopyTo(_tp_origin.tunit, 0);

            string[] time_grid = new string[6];
            time_grid = normalizer.TimeBase.Split('-');

            int tunitpointer = 5;
            while (tunitpointer >= 0 && _tp.tunit[tunitpointer] < 0)
            {
                tunitpointer--;
            }
            for (int i = 0; i < tunitpointer; i++)
            {
                if (_tp.tunit[i] < 0)
                {
                    _tp.tunit[i] = int.Parse(time_grid[i]);
                }
            }
            string[] _result_tmp = new string[6];
            _result_tmp[0] = _tp.tunit[0].ToString();
            if (_tp.tunit[0] >= 10 && _tp.tunit[0] < 100)
            {
                _result_tmp[0] = "19" + _tp.tunit[0].ToString();
            }
            if (_tp.tunit[0] > 0 && _tp.tunit[0] < 10)
            {
                _result_tmp[0] = "200" + _tp.tunit[0].ToString();
            }

            for (int i = 1; i < 6; i++)
            {
                _result_tmp[i] = _tp.tunit[i].ToString();
            }

            DateTime cale = new DateTime(); //leverage a calendar object to figure out the final time
            int year = cale.Year, month = cale.Month, day = cale.Day, hour = cale.Hour, min = cale.Hour, second = cale.Second;
            if (int.Parse(_result_tmp[0]) != -1)
            {
                Time_Norm += _result_tmp[0] + "年";
                year = Convert.ToInt32(_result_tmp[0]);
                if (int.Parse(_result_tmp[1]) != -1)
                {
                    Time_Norm += _result_tmp[1] + "月";
                    month = Convert.ToInt32(_result_tmp[1]);
                    if (int.Parse(_result_tmp[2]) != -1)
                    {
                        Time_Norm += _result_tmp[2] + "日";
                        day = Convert.ToInt32(_result_tmp[2]);
                        if (int.Parse(_result_tmp[3]) != -1)
                        {
                            Time_Norm += _result_tmp[3] + "时";
                            hour = Convert.ToInt32(_result_tmp[3]);
                            if (int.Parse(_result_tmp[4]) != -1)
                            {
                                Time_Norm += _result_tmp[4] + "分";
                                min = Convert.ToInt32(_result_tmp[4]);
                                if (int.Parse(_result_tmp[5]) != -1)
                                {
                                    Time_Norm += _result_tmp[5] + "秒";
                                    second = Convert.ToInt32(_result_tmp[5]);
                                }
                            }
                        }
                    }
                }
            }
            cale = new DateTime(year, month, day, hour, min, second);
            time = cale;

            time_full = new int[6];
            _tp.tunit.CopyTo(time_full, 0);
            // time_origin = _tp_origin.tunit.clone(); comment by kexm
        }

        public virtual bool? IsAllDayTime
        {
            get
            {
                return isAllDayTime;
            }
            set
            {
                this.isAllDayTime = value;
            }
        }

        public override string ToString()
        {
            return Time_Expression + " ---> " + Time_Norm;
        }

        /// <summary>
        /// 如果用户选项是倾向于未来时间，检查checkTimeIndex所指的时间是否是过去的时间，如果是的话，将大一级的时间设为当前时间的+1。
        /// <para>如在晚上说"早上8点看书"，则识别为明天早上; 12月31日说"3号买菜"，则识别为明年1月的3号。 ///</para>
        /// </summary>
        /// <param name="checkTimeIndex">_tp.tunit时间数组的下标</param>
        private void preferFuture(int checkTimeIndex)
        {
            /// <summary>
            ///1. 检查被检查的时间级别之前，是否没有更高级的已经确定的时间，如果有，则不进行处理. </summary>
            for (int i = 0; i < checkTimeIndex; i++)
            {
                if (_tp.tunit[i] != -1)
                {
                    return;
                }
            }
            /// <summary>
            ///2. 根据上下文补充时间 </summary>
            checkContextTime(checkTimeIndex);
            /// <summary>
            ///3. 根据上下文补充时间后再次检查被检查的时间级别之前，是否没有更高级的已经确定的时间，如果有，则不进行倾向处理. </summary>
            for (int i = 0; i < checkTimeIndex; i++)
            {
                if (_tp.tunit[i] != -1)
                {
                    return;
                }
            }
            /// <summary>
            ///4. 确认用户选项 </summary>
            if (!normalizer.PreferFuture)
            {
                return;
            }
            /// <summary>
            ///5. 获取当前时间，如果识别到的时间小于当前时间，则将其上的所有级别时间设置为当前时间，并且其上一级的时间步长+1 </summary>
            DateTime c = new DateTime();
            if (this.normalizer.TimeBase != null)
            {
                string[] ini = this.normalizer.TimeBase.Split('-');
                c = new DateTime(Convert.ToInt32(ini[0]), Convert.ToInt32(ini[1]), Convert.ToInt32(ini[2]), Convert.ToInt32(ini[3]), Convert.ToInt32(ini[4]), Convert.ToInt32(ini[5]));
                // LOGGER.debug(DateUtil.formatDateDefault(c.getTime()));
            }

            int curTime = getTimeValue(c, TUNIT_MAP[checkTimeIndex]);
            if (curTime < _tp.tunit[checkTimeIndex])
            {
                return;
            }
            //准备增加的时间单位是被检查的时间的上一级，将上一级时间+1
            int addTimeUnit = TUNIT_MAP[checkTimeIndex - 1];
            addTimeValue(c, addTimeUnit, 1);

            // _tp.tunit[checkTimeIndex - 1] = c.get(TUNIT_MAP.get(checkTimeIndex - 1));
            for (int i = 0; i < checkTimeIndex; i++)
            {
                _tp.tunit[i] = getTimeValue(c, TUNIT_MAP[i]);
                if (TUNIT_MAP[i] == 5) // 月份
                {
                    ++_tp.tunit[i];
                }
            }
        }

        /// <summary>
        /// 如果用户选项是倾向于未来时间，检查所指的day_of_week是否是过去的时间，如果是的话，设为下周。
        /// <para>如在周五说：周一开会，识别为下周一开会 ///</para>
        /// </summary>
        /// <param name="weekday">识别出是周几（范围1-7）</param>
        private void preferFutureWeek(int weekday, ref DateTime c)
        {
            /// <summary>
            ///1. 确认用户选项 </summary>
            if (!normalizer.PreferFuture)
            {
                return;
            }
            /// <summary>
            ///2. 检查被检查的时间级别之前，是否没有更高级的已经确定的时间，如果有，则不进行倾向处理. </summary>
            int checkTimeIndex = 2;
            for (int i = 0; i < checkTimeIndex; i++)
            {
                if (_tp.tunit[i] != -1)
                {
                    return;
                }
            }
            /// <summary>
            ///获取当前是在周几，如果识别到的时间小于当前时间，则识别时间为下一周 </summary>
            DateTime curC = new DateTime();
            if (this.normalizer.TimeBase != null)
            {
                string[] ini = this.normalizer.TimeBase.Split('-');
                curC = new DateTime(Convert.ToInt32(ini[0]), Convert.ToInt32(ini[1]), Convert.ToInt32(ini[2]), Convert.ToInt32(ini[3]), Convert.ToInt32(ini[4]), Convert.ToInt32(ini[5]));
            }
            int curWeekday = Convert.ToInt32(curC.DayOfWeek);
            if (weekday == 0)
            {
                weekday = 7;
            }
            if (curWeekday < weekday)
            {
                return;
            }
            //准备增加的时间单位是被检查的时间的上一级，将上一级时间+1
            c = c.AddDays(7);
        }

        /// <summary>
        /// 根据上下文时间补充时间信息
        /// </summary>
        private void checkContextTime(int checkTimeIndex)
        {
            for (int i = 0; i < checkTimeIndex; i++)
            {
                if (_tp.tunit[i] == -1 && _tp_origin.tunit[i] != -1)
                {
                    _tp.tunit[i] = _tp_origin.tunit[i];
                }
            }
            /// <summary>
            ///在处理小时这个级别时，如果上文时间是下午的且下文没有主动声明小时级别以上的时间，则也把下文时间设为下午 </summary>
            if (isFirstTimeSolveContext == true && checkTimeIndex == 3 && _tp_origin.tunit[checkTimeIndex] >= 12 && _tp.tunit[checkTimeIndex] < 12)
            {
                _tp.tunit[checkTimeIndex] += 12;
            }
            isFirstTimeSolveContext = false;
        }

        private static int getTimeValue(DateTime date, int timeUnit)
        {
            if (timeUnit == 1)
            {
                return date.Year;
            }
            else if (timeUnit == 2)
            {
                return date.Month;
            }
            else if (timeUnit == 5)
            {
                return date.Day;
            }
            else if (timeUnit == 11)
            {
                return date.Hour;
            }
            else if (timeUnit == 12)
            {
                return date.Minute;
            }
            else if (timeUnit == 13)
            {
                return date.Second;
            }
            return 0;
        }

        private static DateTime addTimeValue(DateTime date, int timeUnit, int value)
        {
            if (timeUnit == 1)
            {
                return date.AddYears(value);
            }
            else if (timeUnit == 2)
            {
                return date.AddMonths(value);
            }
            else if (timeUnit == 5)
            {
                return date.AddDays(value);
            }
            else if (timeUnit == 11)
            {
                return date.AddHours(value);
            }
            else if (timeUnit == 12)
            {
                return date.AddMinutes(value); ;
            }
            else if (timeUnit == 13)
            {
                return date.AddSeconds(value);
            }
            return date;
        }

        private static IDictionary<int, int> TUNIT_MAP = new Dictionary<int, int>();

        static TimeUnit()
        {
            TUNIT_MAP[0] = 1;
            TUNIT_MAP[1] = 2;
            TUNIT_MAP[2] = 5;
            TUNIT_MAP[3] = 11;
            TUNIT_MAP[4] = 12;
            TUNIT_MAP[5] = 13;
        }
    }
}