using System;
using System.Collections.Generic;

using System.Text.RegularExpressions;
using DateUtil = Traceless.Utils.TimeNLP.Util.DateUtil;

namespace Traceless.Utils.TimeNLP.NLP
{
    /// <summary>
    /// <para>新版时间表达式识别的主要工作类</para>
    /// <para>/// @author <a href="mailto:kexm@corp.21cn.com">kexm</a> @since 2016年5月4日</para>
    /// </summary>
    [Serializable]
    public class TimeNormalizer
    {
        private const long serialVersionUID = 463541045644656392L;
        private const string PATTERNS = @"((前|昨|今|明|后)(天|日)?(早|晚)(晨|上|间)?)|(([一二三四五六七八九十|0-9]{1,2})(年|月|个月|日|天|小时|分钟|秒)(前|后))|(\d+个?[年月日天][以之]?[前后])|(\d+个?半?(小时|钟头|h|H))|(半个?(小时|钟头))|(\d+(分钟|min))|([13]刻钟)|((上|这|本|下)+(周|星期)([一二三四五六七天日]|[1-7])?)|((周|星期)([一二三四五六七天日]|[1-7]))|((早|晚)?([0-2]?[0-9](点|时)半)(am|AM|pm|PM)?)|((早|晚)?(\d+[:：]\d+([:：]\d+)*)\s*(am|AM|pm|PM)?)|((早|晚)?([0-2]?[0-9](点|时)[13一三]刻)(am|AM|pm|PM)?)|((早|晚)?(\d+[时点](\d+)?分?(\d+秒?)?)\s*(am|AM|pm|PM)?)|(大+(前|后)天)|(([零一二三四五六七八九十百千万]+|\d+)世)|([0-9]?[0-9]?[0-9]{2}\.((10)|(11)|(12)|([1-9]))\.((?<!\\d))([0-3][0-9]|[1-9]))|(现在)|(届时)|(这个月)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)日)|(晚些时候)|(今年)|(长期)|(以前)|(过去)|(时期)|(时代)|(当时)|(近来)|(([零一二三四五六七八九十百千万]+|\d+)夜)|(当前)|(日(数|多|多少|好几|几|差不多|近|前|后|上|左右))|((\d+)点)|(今年([零一二三四五六七八九十百千万]+|\d+))|(\d+[:：]\d+(分|))|((\d+):(\d+))|(\d+/\d+/\d+)|(未来)|((充满美丽、希望、挑战的)?未来)|(最近)|(早上)|(早(数|多|多少|好几|几|差不多|近|前|后|上|左右))|(日前)|(新世纪)|(小时)|(([0-3][0-9]|[1-9])(日|号))|(明天)|(\d+)月|(([0-3][0-9]|[1-9])[日号])|((数|多|多少|好几|几|差不多|近|前|后|上|左右)周)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)([零一二三四五六七八九十百千万]+|\d+)年)|(每[年月日天小时分秒钟]+)|((\d+分)+(\d+秒)?)|([一二三四五六七八九十]+来?[岁年])|([新?|\d*]世纪末?)|((\d+)时)|(世纪)|(([零一二三四五六七八九十百千万]+|\d+)岁)|(今年)|([星期周]+[一二三四五六七])|(星期([零一二三四五六七八九十百千万]+|\d+))|(([零一二三四五六七八九十百千万]+|\d+)年)|([本后昨当新后明今去前那这][一二三四五六七八九十]?[年月日天])|(早|早晨|早上|上午|中午|午后|下午|晚上|晚间|夜里|夜|凌晨|深夜)|(回归前后)|((\d+点)+(\d+分)?(\d+秒)?左右?)|((\d+)年代)|(本月(\d+))|(第(\d+)天)|((\d+)岁)|((\d+)年(\d+)月)|([去今明]?[年月](底|末))|(([零一二三四五六七八九十百千万]+|\d+)世纪)|(昨天(数|多|多少|好几|几|差不多|近|前|后|上|左右)午)|(年度)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)星期)|(年底)|([下个本]+赛季)|(\d+)月(\d+)日|(\d+)月(\d+)|(今年(\d+)月(\d+)日)|((\d+)月(\d+)日(数|多|多少|好几|几|差不多|近|前|后|上|左右)午(\d+)时)|(今年晚些时候)|(两个星期)|(过去(数|多|多少|好几|几|差不多|近|前|后|上|左右)周)|(本赛季)|(半个(数|多|多少|好几|几|差不多|近|前|后|上|左右))|(稍晚)|((\d+)号晚(数|多|多少|好几|几|差不多|近|前|后|上|左右))|(今(数|多|多少|好几|几|差不多|近|前|后|上|左右)(\d+)年)|(这个时候)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)个小时)|(最(数|多|多少|好几|几|差不多|近|前|后|上|左右)(数|多|多少|好几|几|差不多|近|前|后|上|左右)年)|(凌晨)|((\d+)年(\d+)月(\d+)日)|((\d+)个月)|(今天早(数|多|多少|好几|几|差不多|近|前|后|上|左右))|(第[一二三四五六七八九十\d+]+季)|(当地时间)|(今(数|多|多少|好几|几|差不多|近|前|后|上|左右)([零一二三四五六七八九十百千万]+|\d+)年)|(早晨)|(一段时间)|([本上]周[一二三四五六七])|(凌晨(\d+)点)|(去年(\d+)月(\d+)日)|(年关)|(如今)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)小时)|(当晚)|((\d+)日晚(\d+)时)|(([零一二三四五六七八九十百千万]+|\d+)(数|多|多少|好几|几|差不多|近|前|后|上|左右)午)|(每年(\d+)月(\d+)日)|((\d+)月)|(农历)|(两个小时)|(本周([零一二三四五六七八九十百千万]+|\d+))|(长久)|(清晨)|((\d+)号晚)|(春节)|(星期日)|(圣诞)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)段)|(现年)|(当日)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)分钟)|(\d+(天|日|周|月|年)(后|前))|((文艺复兴|巴洛克|前苏联|前一|暴力和专制|成年时期|古罗马|我们所处的敏感)+时期)|((\d+)[年月天])|(清早)|(两年)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)午)|(昨天(数|多|多少|好几|几|差不多|近|前|后|上|左右)午(\d+)时)|(([零一二三四五六七八九十百千万]+|\d+)(数|多|多少|好几|几|差不多|近|前|后|上|左右)年)|(今(数|多|多少|好几|几|差不多|近|前|后|上|左右)(\d+))|(圣诞节)|(学期)|(\d+来?分钟)|(过去(数|多|多少|好几|几|差不多|近|前|后|上|左右)年)|(星期天)|(夜间)|((\d+)日凌晨)|(([零一二三四五六七八九十百千万]+|\d+)月底)|(当天)|((\d+)日)|(((10)|(11)|(12)|([1-9]))月)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)(数|多|多少|好几|几|差不多|近|前|后|上|左右)年)|(今年(\d+)月份)|(晚(数|多|多少|好几|几|差不多|近|前|后|上|左右)(\d+)时)|(连[年月日夜])|((\d+)年(\d+)月(\d+)日(数|多|多少|好几|几|差不多|近|前|后|上|左右)午)|((一|二|两|三|四|五|六|七|八|九|十|百|千|万|几|多|上|\d+)+个?(天|日|周|月|年)(后|前|半))|((胜利的)日子)|(青春期)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)年)|(早(数|多|多少|好几|几|差不多|近|前|后|上|左右)([零一二三四五六七八九十百千万]+|\d+)点(数|多|多少|好几|几|差不多|近|前|后|上|左右))|([0-9]{4}年)|(周末)|(([零一二三四五六七八九十百千万]+|\d+)个(数|多|多少|好几|几|差不多|近|前|后|上|左右)小时)|(([(小学)|初中?|高中?|大学?|研][一二三四五六七八九十]?(\d+)?)?[上下]半?学期)|(([零一二三四五六七八九十百千万]+|\d+)时期)|(午间)|(次年)|(这时候)|(农历新年)|([春夏秋冬](天|季))|((\d+)天)|(元宵节)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)分)|((\d+)月(\d+)日(数|多|多少|好几|几|差不多|近|前|后|上|左右)午)|(晚(数|多|多少|好几|几|差不多|近|前|后|上|左右)(\d+)时(\d+)分)|(傍晚)|(周([零一二三四五六七八九十百千万]+|\d+))|((数|多|多少|好几|几|差不多|近|前|后|上|左右)午(\d+)时(\d+)分)|(同日)|((\d+)年(\d+)月底)|((\d+)分钟)|((\d+)世纪)|(冬季)|(国庆)|(年代)|(([零一二三四五六七八九十百千万]+|\d+)年半)|(今年年底)|(新年)|(本周)|(当地时间星期([零一二三四五六七八九十百千万]+|\d+))|(([零一二三四五六七八九十百千万]+|\d+)(数|多|多少|好几|几|差不多|近|前|后|上|左右)岁)|(半小时)|(每周)|((重要|最后)?时刻)|(([零一二三四五六七八九十百千万]+|\d+)期间)|(周日)|(晚(数|多|多少|好几|几|差不多|近|前|后|上|左右))|(今后)|(([零一二三四五六七八九十百千万]+|\d+)段时间)|(明年)|([12][09][0-9]{2}(年度?))|(([零一二三四五六七八九十百千万]+|\d+)生)|(今天凌晨)|(过去(\d+)年)|(元月)|((\d+)月(\d+)日凌晨)|([前去今明后新]+年)|(\d+)月(\d+)(日?)|(夏天)|((\d+)日凌晨(\d+)时许)|((\d+)月(\d+)日)|((\d+)点半)|(去年底)|(最后一[天刻])|(最(数|多|多少|好几|几|差不多|近|前|后|上|左右)(数|多|多少|好几|几|差不多|近|前|后|上|左右)个月)|(圣诞节?)|(下?个?(星期|周)(一|二|三|四|五|六|七|天))|((\d+)(数|多|多少|好几|几|差不多|近|前|后|上|左右)年)|(当天(数|多|多少|好几|几|差不多|近|前|后|上|左右)午)|(每年的(\d+)月(\d+)日)|((\d+)日晚(数|多|多少|好几|几|差不多|近|前|后|上|左右))|(星期([零一二三四五六七八九十百千万]+|\d+)晚)|(深夜)|(现如今)|([上中下]+午)|(昨晚)|(近年)|(今天清晨)|(中旬)|(星期([零一二三四五六七八九十百千万]+|\d+)早)|(([零一二三四五六七八九十百千万]+|\d+)战期间)|(星期)|(昨天晚(数|多|多少|好几|几|差不多|近|前|后|上|左右))|(较早时)|(个(数|多|多少|好几|几|差不多|近|前|后|上|左右)小时)|((民主高中|我们所处的|复仇主义和其它危害人类的灾难性疾病盛行的|快速承包电影主权的|恢复自我美德|人类审美力基础设施|饱受暴力、野蛮、流血、仇恨、嫉妒的|童年|艰苦的童年)+时代)|(元旦)|(([零一二三四五六七八九十百千万]+|\d+)个礼拜)|(昨日)|([年月]初)|((\d+)年的(\d+)月)|(每年)|(([零一二三四五六七八九十百千万]+|\d+)月份)|(今年(\d+)月(\d+)号)|(今年([零一二三四五六七八九十百千万]+|\d+)月)|((\d+)月底)|(未来(\d+)年)|(第([零一二三四五六七八九十百千万]+|\d+)季)|(\d?多年)|(([零一二三四五六七八九十百千万]+|\d+)个星期)|((\d+)年([零一二三四五六七八九十百千万]+|\d+)月)|([下上中]午)|(早(数|多|多少|好几|几|差不多|近|前|后|上|左右)(\d+)点)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)月)|(([零一二三四五六七八九十百千万]+|\d+)个(数|多|多少|好几|几|差不多|近|前|后|上|左右)月)|(同([零一二三四五六七八九十百千万]+|\d+)天)|((\d+)号凌晨)|(夜里)|(两个(数|多|多少|好几|几|差不多|近|前|后|上|左右)小时)|(昨天)|(罗马时代)|(目(数|多|多少|好几|几|差不多|近|前|后|上|左右))|(([零一二三四五六七八九十百千万]+|\d+)月)|((\d+)年(\d+)月(\d+)号)|(((10)|(11)|(12)|([1-9]))月份?)|([12][0-9]世纪)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)([零一二三四五六七八九十百千万]+|\d+)天)|(工作日)|(稍后)|((\d+)号(数|多|多少|好几|几|差不多|近|前|后|上|左右)午)|(未来([零一二三四五六七八九十百千万]+|\d+)年)|(([零一二三四五六七八九十百千万]+|\d+)日(数|多|多少|好几|几|差不多|近|前|后|上|左右)午)|(最(数|多|多少|好几|几|差不多|近|前|后|上|左右)([零一二三四五六七八九十百千万]+|\d+)刻)|(很久)|((\d+)(数|多|多少|好几|几|差不多|近|前|后|上|左右)岁)|(去年(\d+)月(\d+)号)|(两个月)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)午(\d+)时)|(古代)|(两天)|(\d+个?(小时|星期))|((\d+)年半)|(较早)|(([零一二三四五六七八九十百千万]+|\d+)个小时)|(星期([零一二三四五六七八九十百千万]+|\d+)(数|多|多少|好几|几|差不多|近|前|后|上|左右)午)|(时刻)|((\d+天)+(\d+点)?(\d+分)?(\d+秒)?)|((\d+)日([零一二三四五六七八九十百千万]+|\d+)时)|(([零一二三四五六七八九十百千万]+|\d+)早)|(([零一二三四五六七八九十百千万]+|\d+)日)|(去年(\d+)月)|(过去([零一二三四五六七八九十百千万]+|\d+)年)|((\d+)个星期)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)(数|多|多少|好几|几|差不多|近|前|后|上|左右)天)|(执政期间)|([当前昨今明后春夏秋冬]+天)|(去年(\d+)月份)|(今(数|多|多少|好几|几|差不多|近|前|后|上|左右))|(两星期)|(([零一二三四五六七八九十百千万]+|\d+)年代)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)天)|(昔日)|(两个半月)|([印尼|北京|美国]?当地时间)|(连日)|(本月(\d+)日)|(第([零一二三四五六七八九十百千万]+|\d+)天)|((\d+)点(\d+)分)|([长近多]年)|((\d+)日(数|多|多少|好几|几|差不多|近|前|后|上|左右)午(\d+)时)|(那时)|(冷战时代)|(([零一二三四五六七八九十百千万]+|\d+)天)|(这个星期)|(去年)|(昨天傍晚)|(近期)|(星期([零一二三四五六七八九十百千万]+|\d+)早些时候)|((\d+)([零一二三四五六七八九十百千万]+|\d+)年)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)两个月)|((\d+)个小时)|(([零一二三四五六七八九十百千万]+|\d+)个月)|(当年)|(本月)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)([零一二三四五六七八九十百千万]+|\d+)个月)|((\d+)点(数|多|多少|好几|几|差不多|近|前|后|上|左右))|(目前)|(去年([零一二三四五六七八九十百千万]+|\d+)月)|((\d+)时(\d+)分)|(每月)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)段时间)|((\d+)日晚)|(早(数|多|多少|好几|几|差不多|近|前|后|上|左右)(\d+)点(数|多|多少|好几|几|差不多|近|前|后|上|左右))|(下旬)|((\d+)月份)|(逐年)|(稍(数|多|多少|好几|几|差不多|近|前|后|上|左右))|((\d+)年)|(月底)|(这个月)|((\d+)年(\d+)个月)|(\d+大寿)|(周([零一二三四五六七八九十百千万]+|\d+)早(数|多|多少|好几|几|差不多|近|前|后|上|左右))|(半年)|(今日)|(末日)|(昨天深夜)|(今年(\d+)月)|((\d+)月(\d+)号)|((\d+)日夜)|((早些|某个|晚间|本星期早些|前些)+时候)|(同年)|((北京|那个|更长的|最终冲突的)时间)|(每个月)|(一早)|((\d+)来?[岁年])|((数|多|多少|好几|几|差不多|近|前|后|上|左右)个月)|([鼠牛虎兔龙蛇马羊猴鸡狗猪]年)|(季度)|(早些时候)|(今天)|(每天)|(年半)|(下(个)?月)|(午后)|((\d+)日(数|多|多少|好几|几|差不多|近|前|后|上|左右)午)|((数|多|多少|好几|几|差不多|近|前|后|上|左右)个星期)|(今天(数|多|多少|好几|几|差不多|近|前|后|上|左右)午)|(同[一二三四五六七八九十][年|月|天])|(T\d+:\d+:\d+)|(\d+/\d+/\d+:\d+:\d+.\d+)|(\?\?\?\?-\?\?-\?\?T\d+:\d+:\d+)|(\d+-\d+-\d+T\d+:\d+:\d+)|(\d+/\d+/\d+ \d+:\d+:\d+.\d+)|(\d+-\d+-\d+|[0-9]{8})|(((\d+)年)?((10)|(11)|(12)|([1-9]))月(\d+))|((\d[\.\-])?((10)|(11)|(12)|([1-9]))[\.\-](\d+))";

        private string timeBase;
        private string oldTimeBase;
        private static Regex patterns = null;
        private string target;
        private TimeUnit[] timeToken = new TimeUnit[0];

        private bool isPreferFuture = true;

        public TimeNormalizer()
        {
            if (patterns == null)
            {
                try
                {
                    patterns = readModel();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                    Console.Error.Write("Read model error!");
                }
            }
        }

        /// <summary>
        /// 参数为TimeExp.m文件路径
        /// </summary>
        /// <param name="path"></param>
        public TimeNormalizer(bool isPreferFuture)
        {
            this.isPreferFuture = isPreferFuture;
            if (patterns == null)
            {
                try
                {
                    patterns = readModel();
                }
                catch (Exception e)
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine(e.ToString());
                    Console.Write(e.StackTrace);
                    Console.Error.Write("Read model error!");
                }
            }
        }

        /// <summary>
        /// TimeNormalizer的构造方法，根据提供的待分析字符串和timeBase进行时间表达式提取 在构造方法中已完成对待分析字符串的表达式提取工作
        /// </summary>
        /// <param name="target">待分析字符串</param>
        /// <param name="timeBase">给定的timeBase</param>
        /// <returns>返回值</returns>
        public virtual TimeUnit[] parse(string target, string timeBase)
        {
            this.target = target;
            this.timeBase = timeBase;
            this.oldTimeBase = timeBase;
            // 字符串预处理
            preHandling();
            timeToken = TimeEx(this.target, timeBase);
            return timeToken;
        }

        /// <summary>
        /// 同上的TimeNormalizer的构造方法，timeBase取默认的系统当前时间
        /// </summary>
        /// <param name="target">待分析字符串</param>
        /// <returns>时间单元数组</returns>
        public virtual TimeUnit[] parse(string target)
        {
            this.target = target;
            this.timeBase = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            // Calendar.getInstance().getTime()换成new Date？
            this.oldTimeBase = timeBase;
            preHandling(); // 字符串预处理
            timeToken = TimeEx(this.target, timeBase);
            return timeToken;
        }

        /// <summary>
        /// timeBase的get方法
        /// </summary>
        /// <returns>返回值</returns>
        public virtual string TimeBase
        {
            get
            {
                return timeBase;
            }
            set
            {
                timeBase = value;
            }
        }

        /// <summary>
        /// oldTimeBase的get方法
        /// </summary>
        /// <returns>返回值</returns>
        public virtual string OldTimeBase
        {
            get
            {
                return oldTimeBase;
            }
        }

        public virtual bool PreferFuture
        {
            get
            {
                return isPreferFuture;
            }
            set
            {
                this.isPreferFuture = value;
            }
        }

        /// <summary>
        /// 重置timeBase为oldTimeBase
        /// </summary>
        public virtual void resetTimeBase()
        {
            timeBase = oldTimeBase;
        }

        /// <summary>
        /// 时间分析结果以TimeUnit组的形式出现，此方法为分析结果的get方法
        /// </summary>
        /// <returns>返回值</returns>
        public virtual TimeUnit[] TimeUnit
        {
            get
            {
                return timeToken;
            }
        }

        /// <summary>
        /// 待匹配字符串的清理空白符和语气助词以及大写数字转化的预处理
        /// </summary>
        private void preHandling()
        {
            target = stringPreHandlingModule.delKeyword(target, "\\s+"); // 清理空白符
            target = stringPreHandlingModule.delKeyword(target, "[的]+"); // 清理语气助词
            target = stringPreHandlingModule.numberTranslator(target); // 大写数字转化
            // TODO 处理大小写标点符号
        }

        /// <summary>
        /// 有基准时间输入的时间表达式识别
        /// <para>
        /// 这是时间表达式识别的主方法， 通过已经构建的正则表达式对字符串进行识别，并按照预先定义的基准时间进行规范化 将所有别识别并进行规范化的时间表达式进行返回，
        /// 时间表达式通过TimeUnit类进行定义 ///
        /// </para>
        /// </summary>
        /// <param name="String">输入文本字符串</param>
        /// <param name="String">输入基准时间</param>
        /// <returns>TimeUnit[] 时间表达式类型数组</returns>
        private TimeUnit[] TimeEx(string tar, string timebase)
        {
            Match match;
            int startline = -1, endline = -1;

            string[] temp = new string[99];
            int rpointer = 0; // 计数器，记录当前识别到哪一个字符串了
            TimeUnit[] Time_Result = null;

            match = patterns.Match(tar);
            bool startmark = true;
            while (match.Success)
            {
                startline = match.Index;
                if (endline == startline) // 假如下一个识别到的时间字段和上一个是相连的 @author kexm
                {
                    rpointer--;
                    temp[rpointer] = temp[rpointer] + match.Value; // 则把下一个识别到的时间字段加到上一个时间字段去
                }
                else
                {
                    if (!startmark)
                    {
                        rpointer--;
                        rpointer++;
                    }
                    startmark = false;
                    temp[rpointer] = match.Value; // 记录当前识别到的时间字段，并把startmark开关关闭。这个开关貌似没用？
                }
                endline = match.Index + match.Length;
                rpointer++;
                match = match.NextMatch();
            }
            if (rpointer > 0)
            {
                rpointer--;
                rpointer++;
            }
            Time_Result = new TimeUnit[rpointer];
            /// <summary>
            ///时间上下文： 前一个识别出来的时间会是下一个时间的上下文，用于处理：周六3点到5点这样的多个时间的识别，第二个5点应识别到是周六的。 </summary>
            TimePoint contextTp = new TimePoint();
            for (int j = 0; j < rpointer; j++)
            {
                Time_Result[j] = new TimeUnit(temp[j], this, contextTp);
                contextTp = Time_Result[j]._tp;
            }
            /// <summary>
            ///过滤无法识别的字段 </summary>
            Time_Result = filterTimeUnit(Time_Result);
            return Time_Result;
        }

        /// <summary>
        /// 过滤timeUnit中无用的识别词。无用识别词识别出的时间是1970.01.01 00:00:00(fastTime=-28800000)
        /// </summary>
        /// <param name="timeUnit">@return</param>
        public static TimeUnit[] filterTimeUnit(TimeUnit[] timeUnit)
        {
            if (timeUnit == null || timeUnit.Length < 1)
            {
                return timeUnit;
            }
            IList<TimeUnit> list = new List<TimeUnit>();
            foreach (TimeUnit t in timeUnit)
            {
                if (t.Time.Ticks != 0)
                {
                    list.Add(t);
                }
            }
            TimeUnit[] newT = new TimeUnit[list.Count];
            list.CopyTo(newT, 0);
            return newT;
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: private java.util.regex.Pattern readModel(java.io.ObjectInputStream in) throws Exception
        private Regex readModel()
        {
            return new Regex(PATTERNS);
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public static void writeModel(Object p, String path) throws Exception
        public static void writeModel(object p, string path)
        {
        }

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public static void main(String args[]) throws Exception
        public static void MainTest(string[] args)
        {
            TimeNormalizer normalizer = new TimeNormalizer();

            normalizer.parse("本周日到下周日出差"); // 抽取时间
            TimeUnit[] unit = normalizer.TimeUnit;
            /// <summary>
            ///写TimeExp </summary>
            // Pattern p = Pattern.compile(); writeModel(p, classPath+"/TimeExp1.zip");
            /// <summary>
            ///测试新增正则 </summary>
            // Pattern p = Pattern.compile("(月|\\.|\\-)"); Matcher m = p.matcher("17年2月1 中午吃饭");
            // if(m.find()){ LOGGER.debug(m.group()); }
        }
    }
}