using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Traceless.OPQSDK.Models.Msg
{
    public class OPQCode
    {
        #region --字段--

        private static readonly Lazy<Regex[]> _regices = new Lazy<Regex[]>(InitializeRegex);

        private string _originalString;
        private OPQFunction _type;
        private bool _convert = true;
        private Dictionary<string, string> _items;

        #endregion --字段--

        #region --属性--

        /// <summary>
        /// 获取一个值, 指示当前实例的功能
        /// </summary>
        public OPQFunction Function { get { return _type; } }

        /// <summary>
        /// 获取当前实例所包含的所有项目
        /// </summary>
        public Dictionary<string, string> Items { get { return _items; } }

        /// <summary>
        /// 获取一个值, 指示当前实例是否属于图片 <see cref="OPQCode"/>
        /// </summary>
        public bool IsImageOPQCode { get { return EqualIsImageOPQCode(this); } }

        #endregion --属性--

        #region --构造函数--

        /// <summary>
        /// 使用 OPQ码 字符串初始化 <see cref="OPQCode"/> 类的新实例
        /// </summary>
        /// <param name="str">OPQ码字符串 或 包含OPQ码的字符串</param>
        private OPQCode(string str)
        {
            this._originalString = str;

            #region --解析 OPQCode--

            Match match = _regices.Value[0].Match(str);
            if (!match.Success)
            {
                throw new FormatException("无法解析所传入的字符串, 字符串非OPQ码格式!");
            }

            #endregion --解析 OPQCode--

            #region --解析OPQ码类型--

            if (!System.Enum.TryParse<OPQFunction>(match.Groups[1].Value, true, out _type))
            {
                this._type = OPQFunction.Unknown;    // 解析不出来的时候, 直接给一个默认
            }

            #endregion --解析OPQ码类型--

            #region --解析键值对--

            MatchCollection collection = _regices.Value[1].Matches(match.Groups[2].Value);
            this._items = new Dictionary<string, string>(collection.Count);
            foreach (Match item in collection)
            {
                this._items.Add(item.Groups[1].Value, OPQDeCode(item.Groups[2].Value));
            }

            #endregion --解析键值对--
        }

        public static string OPQDeCode(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            StringBuilder builder = new StringBuilder(source);
            builder = builder.Replace("&#91;", "[");
            builder = builder.Replace("&#93;", "]");
            builder = builder.Replace("&#44;", ",");
            builder = builder.Replace("&amp;", "&");
            return builder.ToString();
        }

        /// <summary>
        /// 初始化 <see cref="OPQCode"/> 类的新实例
        /// </summary>
        /// <param name="type">OPQ码类型</param>
        /// <param name="keyValues">包含的键值对</param>
        public OPQCode(OPQFunction type, params KeyValuePair<string, string>[] keyValues)
        {
            this._type = type;
            this._items = new Dictionary<string, string>(keyValues.Length);
            foreach (KeyValuePair<string, string> item in keyValues)
            {
                this._items.Add(item.Key, item.Value);
            }

            this._originalString = null;
        }

        #endregion --构造函数--

        #region --公开方法--

        /// <summary>
        /// 从字符串中解析出所有的 OPQ码, 转换为 <see cref="OPQCode"/> 集合
        /// </summary>
        /// <param name="source">原始字符串</param>
        /// <returns>返回等效的 <see cref="List{OPQCode}"/></returns>
        public static List<OPQCode> Parse(string source)
        {
            MatchCollection collection = _regices.Value[0].Matches(source);
            List<OPQCode> codes = new List<OPQCode>(collection.Count);
            foreach (Match item in collection)
            {
                codes.Add(new OPQCode(item.Groups[0].Value));
            }
            return codes;
        }

        /// <summary>
        /// 判断是否是图片 <see cref="OPQCode"/>
        /// </summary>
        /// <param name="code">要判断的 <see cref="OPQCode"/> 实例</param>
        /// <returns>如果是图片 <see cref="OPQCode"/> 返回 <see langword="true"/> 否则返回 <see langword="false"/></returns>
        public static bool EqualIsImageOPQCode(OPQCode code)
        {
            return code.Function == OPQFunction.Pic;
        }

        /// <summary>
        /// 确定指定的对象是否等于当前对象
        /// </summary>
        /// <param name="obj">要与当前对象进行比较的对象</param>
        /// <returns>
        /// 如果指定的对象等于当前对象，则为
        /// <code>true</code>
        /// ，否则为
        /// <code>false</code>
        /// </returns>
        public override bool Equals(object obj)
        {
            OPQCode code = obj as OPQCode;
            if (code != null)
            {
                return string.Equals(this._originalString, code._originalString);
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// 返回该字符串的哈希代码
        /// </summary>
        /// <returns>32 位有符号整数哈希代码</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode() & this._originalString.GetHashCode();
        }

        /// <summary>
        /// 返回此实例等效的OPQ码形式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this._originalString == null)
            {
                if (this._items.Count == 0)
                {
                    return "";
                }
                else
                {
                    // 普通OPQ码, 带参数
                    StringBuilder builder = new StringBuilder();
                    builder.Append("[CODE:");
                    builder.Append(this._type.GetDescription());   // function
                    foreach (KeyValuePair<string, string> item in this._items)
                    {
                        builder.AppendFormat(",{0}={1}", item.Key, OPQEnCode(item.Value, true));
                    }
                    builder.Append("]");
                    this._originalString = builder.ToString();
                }
            }
            return this._originalString;
        }

        public static string OPQEnCode(string source, bool enCodeComma)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            StringBuilder builder = new StringBuilder(source);
            builder = builder.Replace("&", "&amp;");
            builder = builder.Replace("[", "&#91;");
            builder = builder.Replace("]", "&#93;");
            if (enCodeComma)
            {
                builder = builder.Replace(",", "&#44;");
            }
            return builder.ToString();
        }

        #endregion --公开方法--

        #region --私有方法--

        /// <summary>
        /// 延时初始化正则表达式
        /// </summary>
        /// <returns></returns>
        private static Regex[] InitializeRegex()
        {
            // 此处延时加载, 以提升运行速度
            return new Regex[]
            {
                new Regex(@"\[CODE:([A-Za-z]*)(?:(,[^\[\]]+))?\]", RegexOptions.Compiled),    // 匹配OPQ码
                new Regex(@",([A-Za-z]+)=([^,\[\]]+)", RegexOptions.Compiled)               // 匹配键值对
            };
        }

        #endregion --私有方法--
    }
}