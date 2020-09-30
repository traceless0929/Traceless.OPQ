using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traceless.Utils.Ai.Tencent.Model
{
    public class TexSmartResp
    {
        /// <summary>
        /// 调用执行后返回的辅助信息
        /// </summary>
        public Header header { get; set; }

        /// <summary>
        /// 输入句子正则化的结果
        /// </summary>
        public string norm_str { get; set; }

        public int lang { get; set; }

        /// <summary>
        /// 基础粒度分词和词性标注的结果
        /// </summary>
        public WordItemBase[] word_list { get; set; }

        /// <summary>
        /// 复合粒度分词和词性标注的结果
        /// </summary>
        public WordItemBase[] phrase_list { get; set; }

        /// <summary>
        /// 识别出的实体信息
        /// </summary>
        public EntityItem[] entity_list { get; set; }

        /// <summary>
        /// 成分句法分析的结果
        /// </summary>
        public string syntactic_parsing_str { get; set; }

        /// <summary>
        /// 语义角色标注的结果
        /// </summary>
        public string srl_str { get; set; }

        /// <summary>
        /// 获取语义中的时间
        /// </summary>
        public List<DateTime> FindTimes
        {
            get
            {
                return entity_list.Where(p => p.type.name.Contains("time.generic")).Select(p =>
                {
                    int[] valArr = p.meaning.value;
                    return Utils.parseTexDtArr(valArr);
                }).ToList();
            }
        }
    }

    public class Header
    {
        /// <summary>
        /// 处理请求所花的时间，以毫秒（ms）计算
        /// </summary>
        public float time_cost_ms { get; set; }

        /// <summary>
        /// 处理请求所花的时间，以秒来计算
        /// </summary>
        public float time_cost { get; set; }

        public float core_time_cost_ms { get; set; }

        /// <summary>
        /// 返回码，"succ"表示成功
        /// </summary>
        public string ret_code { get; set; }
    }

    public class WordItemBase
    {
        public string str { get; set; }
        public int[] hit { get; set; }
        public string tag { get; set; }
    }

    public class EntityItem : WordItemBase
    {
        public Type type { get; set; }
        public Meaning meaning { get; set; }
        public string tag_i18n { get; set; }
    }

    public class Type
    {
        public string name { get; set; }
        public string i18n { get; set; }
        public string path { get; set; }
    }

    public class Meaning
    {
        public int[] value { get; set; }
    }
}