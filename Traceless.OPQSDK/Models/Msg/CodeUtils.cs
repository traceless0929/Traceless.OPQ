using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Traceless.OPQSDK.Models.Msg
{
    public static class CodeUtils
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
            if (atList.Length == 1 && atList[0] == -1)
            {
                return $"[ATALL()]";
            }
            return $"[ATUSER({string.Join(",", atList)})]";
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

        /// <summary>
        /// 图片码，会自动转换为url中的图片进行发送
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static OPQCode Pic_Http(string url)
        {
            return new OPQCode(OPQFunction.Pic, new KeyValuePair<string, string>("url", url));
        }

        /// <summary>
        /// 语音码，会自动转换为url中的语音进行发送
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static OPQCode Voice_Http(string url)
        {
            return new OPQCode(OPQFunction.Voice, new KeyValuePair<string, string>("url", url));
        }

        /// <summary>
        /// 分享卡片
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="desc">描述</param>
        /// <param name="prompt">聊天列表的缩略消息</param>
        /// <param name="tag">左下角标志</param>
        /// <param name="url">跳转地址</param>
        /// <param name="preview">缩略图</param>
        /// <returns></returns>
        public static OPQCode Rich(string title, string desc = "", string prompt = "[卡片消息]", string tag = "", string url = "", string preview = "")
        {
            return new OPQCode(OPQFunction.Rich,
                new KeyValuePair<string, string>("url", url),
                new KeyValuePair<string, string>("title", title),
                new KeyValuePair<string, string>("desc", desc),
                new KeyValuePair<string, string>("prompt", prompt),
                new KeyValuePair<string, string>("preview", preview),
                new KeyValuePair<string, string>("tag", tag)
                );
        }

        /// <summary>
        /// 解析文本中的OPQ码
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public static List<OPQCode> ParseOPQCode(this string raw)
        {
            return OPQCode.Parse(raw);
        }

        public static List<string> GetRegexStr(string reString, string regexCode)
        {
            //注意 reString 请替换为需要处理的字符串
            List<string> strList = new List<string>();
            var reg = new Regex(regexCode);
            MatchCollection mc = reg.Matches(reString);
            for (int i = 0; i < mc.Count; i++)
            {
                GroupCollection gc = mc[i].Groups; //得到所有分组
                for (int j = 1; j < gc.Count; j++) //多分组 匹配的原始文本不要
                {
                    string temp = gc[j].Value;
                    if (!string.IsNullOrEmpty(temp))
                    {
                        strList.Add(temp); //获取结果   strList中为匹配的值
                    }
                }
            }
            //需要获取匹配的数据,请遍历strList  通常情况下(正则表达式中只有一个分组),只需要取strList[1]即可. 如果有多个分组,依次类推即可.
            return strList;
        }

        /// <summary>
        /// 读取 <see cref="System.Enum"/> 标记 <see cref="System.ComponentModel.DescriptionAttribute"/> 的值
        /// </summary>
        /// <param name="value">原始 <see cref="System.Enum"/> 值</param>
        /// <returns></returns>
		public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>(false);
            return attribute.Description;
        }
    }
}