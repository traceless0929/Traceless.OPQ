using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Traceless.Utils.TimeNLP.NLP
{
    /// <summary>
    /// 字符串预处理模块，为分析器TimeNormalizer提供相应的字符串预处理服务 /// @author 曹零07300720158 ///
    /// </summary>
    public class stringPreHandlingModule
    {
        /// <summary>
        /// 该方法删除一字符串中所有匹配某一规则字串 可用于清理一个字符串中的空白符和语气助词
        /// </summary>
        /// <param name="target">待处理字符串</param>
        /// <param name="rules">删除规则</param>
        /// <returns>清理工作完成后的字符串</returns>
        public static string delKeyword(string target, string rules)
        {
            Regex p = new Regex(rules);
            return p.Replace(target, "");
        }

        /// <summary>
        /// 该方法可以将字符串中所有的用汉字表示的数字转化为用阿拉伯数字表示的数字 如"这里有一千两百个人，六百零五个来自中国"可以转化为 "这里有1200个人，605个来自中国"
        /// 此外添加支持了部分不规则表达方法 如两万零六百五可转化为20650 两百一十四和两百十四都可以转化为214 一六零加一五八可以转化为160+158
        /// 该方法目前支持的正确转化范围是0-99999999 该功能模块具有良好的复用性
        /// </summary>
        /// <param name="target">待转化的字符串</param>
        /// <returns>转化完毕后的字符串</returns>
        public static string numberTranslator(string target)
        {
            Regex p = new Regex("[一二两三四五六七八九123456789]万[一二两三四五六七八九123456789](?!(千|百|十))");
            Match m = p.Match(target);

            while (m.Success)
            {
                string[] s = m.Value.Split("万", true);
                int num = 0;
                if (s.Length == 2)
                {
                    num += wordToNumber(s[0]) * 10000 + wordToNumber(s[1]) * 1000;
                }
                target = p.Replace(target, num.ToString(), 1, m.Index);
                m = m.NextMatch();
            }

            p = new Regex("[一二两三四五六七八九123456789]千[一二两三四五六七八九123456789](?!(百|十))");
            m = p.Match(target);
            while (m.Success)
            {
                string[] s = m.Value.Split("千", true);
                int num = 0;
                if (s.Length == 2)
                {
                    num += wordToNumber(s[0]) * 1000 + wordToNumber(s[1]) * 100;
                }
                target = p.Replace(target, num.ToString(), 1, m.Index);
                m = m.NextMatch();
            }

            p = new Regex("[一二两三四五六七八九123456789]百[一二两三四五六七八九123456789](?!十)");
            m = p.Match(target);
            while (m.Success)
            {
                string[] s = m.Value.Split("百", true);
                int num = 0;
                if (s.Length == 2)
                {
                    num += wordToNumber(s[0]) * 100 + wordToNumber(s[1]) * 10;
                }
                target = p.Replace(target, num.ToString(), 1, m.Index);
                m = m.NextMatch();
            }

            p = new Regex("[零一二两三四五六七八九]");
            m = p.Match(target);
            while (m.Success)
            {
                target = p.Replace(target, Convert.ToString(wordToNumber(m.Value)), 1, m.Index);
                m = m.NextMatch();
            }

            p = new Regex("(?<=(周|星期))[末天日]");
            m = p.Match(target);
            while (m.Success)
            {
                target = p.Replace(target, Convert.ToString(wordToNumber(m.Value)), 1, m.Index);
                m = m.NextMatch();
            }

            p = new Regex("(?<!(周|星期))0?[0-9]?十[0-9]?");
            m = p.Match(target);
            while (m.Success)
            {
                string[] s = m.Value.Split("十", true);
                int num = 0;
                if (s.Length == 0)
                {
                    num += 10;
                }
                else if (s.Length == 1)
                {
                    int ten = int.Parse(s[0]);
                    if (ten == 0)
                    {
                        num += 10;
                    }
                    else
                    {
                        num += ten * 10;
                    }
                }
                else if (s.Length == 2)
                {
                    if (s[0].Equals(""))
                    {
                        num += 10;
                    }
                    else
                    {
                        int ten = int.Parse(s[0]);
                        if (ten == 0)
                        {
                            num += 10;
                        }
                        else
                        {
                            num += ten * 10;
                        }
                    }
                    num += int.Parse(s[1]);
                }
                target = p.Replace(target, Convert.ToString(num), 1, m.Index);
                m = m.NextMatch();
            }

            p = new Regex("0?[1-9]百[0-9]?[0-9]?");
            m = p.Match(target);
            while (m.Success)
            {
                string[] s = m.Value.Split("百", true);
                int num = 0;
                if (s.Length == 1)
                {
                    int hundred = int.Parse(s[0]);
                    num += hundred * 100;
                }
                else if (s.Length == 2)
                {
                    int hundred = int.Parse(s[0]);
                    num += hundred * 100;
                    num += int.Parse(s[1]);
                }
                target = p.Replace(target, Convert.ToString(num), 1, m.Index);
                m = m.NextMatch();
            }

            p = new Regex("0?[1-9]千[0-9]?[0-9]?[0-9]?");
            m = p.Match(target);
            while (m.Success)
            {
                string group = m.Value;
                string[] s = group.Split("千", true);
                int num = 0;
                if (s.Length == 1)
                {
                    int thousand = int.Parse(s[0]);
                    num += thousand * 1000;
                }
                else if (s.Length == 2)
                {
                    int thousand = int.Parse(s[0]);
                    num += thousand * 1000;
                    num += int.Parse(s[1]);
                }
                target = p.Replace(target, Convert.ToString(num), 1, m.Index);
                m = m.NextMatch();
            }

            p = new Regex("[0-9]+万[0-9]?[0-9]?[0-9]?[0-9]?");
            m = p.Match(target);
            while (m.Success)
            {
                string[] s = m.Value.Split("万", true);
                int num = 0;
                if (s.Length == 1)
                {
                    int tenthousand = int.Parse(s[0]);
                    num += tenthousand * 10000;
                }
                else if (s.Length == 2)
                {
                    int tenthousand = int.Parse(s[0]);
                    num += tenthousand * 10000;
                    num += int.Parse(s[1]);
                }
                target = p.Replace(target, Convert.ToString(num), 1, m.Index);
                m = m.NextMatch();
            }

            return target;
        }

        /// <summary>
        /// 方法numberTranslator的辅助方法，可将[零-九]正确翻译为[0-9]
        /// </summary>
        /// <param name="s">大写数字</param>
        /// <returns>对应的整形数，如果不是大写数字返回-1</returns>
        private static int wordToNumber(string s)
        {
            if (s.Equals("零") || s.Equals("0"))
            {
                return 0;
            }
            else if (s.Equals("一") || s.Equals("1"))
            {
                return 1;
            }
            else if (s.Equals("二") || s.Equals("两") || s.Equals("2"))
            {
                return 2;
            }
            else if (s.Equals("三") || s.Equals("3"))
            {
                return 3;
            }
            else if (s.Equals("四") || s.Equals("4"))
            {
                return 4;
            }
            else if (s.Equals("五") || s.Equals("5"))
            {
                return 5;
            }
            else if (s.Equals("六") || s.Equals("6"))
            {
                return 6;
            }
            else if (s.Equals("七") || s.Equals("天") || s.Equals("日") || s.Equals("末") || s.Equals("7"))
            {
                return 7;
            }
            else if (s.Equals("八") || s.Equals("8"))
            {
                return 8;
            }
            else if (s.Equals("九") || s.Equals("9"))
            {
                return 9;
            }
            else
            {
                return -1;
            }
        }
    }
}