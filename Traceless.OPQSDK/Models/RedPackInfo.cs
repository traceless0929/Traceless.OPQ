using System;

namespace Traceless.OPQSDK.Models
{
    public class RedPackInfo
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Tittle { get; set; }

        /// <summary>
        /// 描述内容
        /// </summary>
        public string Des { get; set; }

        /// <summary>
        /// 红包类型 1普通红包 2口令红包 3语音红包 4表情红包 5成语红包 6专属红包
        /// </summary>
        public int RedType { get; set; }

        /// <summary>
        /// 列表ID
        /// </summary>
        public string Listid { get; set; }

        /// <summary>
        /// 认证key
        /// </summary>
        public string Authkey { get; set; }

        /// <summary>
        /// 频道
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 开始索引
        /// </summary>
        public string StingIndex { get; set; }

        /// <summary>
        /// 未知
        /// </summary>
        public int GroupID { get; set; }

        /// <summary>
        /// 未知
        /// </summary>
        public int SelfUin { get; set; }

        /// <summary>
        /// token
        /// </summary>
        public string Token_17_2 { get; set; }

        /// <summary>
        /// token
        /// </summary>

        public string Token_17_3 { get; set; }

        /// <summary>
        /// 来自
        /// </summary>
        public long FromUin { get; set; }

        /// <summary>
        /// 来源类型 0好友
        /// </summary>
        public int FromType { get; set; }

        /// <summary>
        /// 是否为转账
        /// </summary>
        /// <returns></returns>
        public bool IsPay()
        {
            return this.Authkey.Length == 0 && this.StingIndex == null && this.Token_17_2 == null && this.Token_17_3 == null && this.Tittle.Contains("元") && this.Des.Contains("已转入你的余额");
        }

        /// <summary>
        /// 获取转账金额
        /// </summary>
        /// <returns>元</returns>
        public decimal PayAmt()
        {
            if (!IsPay())
            {
                return 0;
            }
            return Convert.ToDecimal(this.Tittle.Replace("元", "").Trim());
        }
    }
}