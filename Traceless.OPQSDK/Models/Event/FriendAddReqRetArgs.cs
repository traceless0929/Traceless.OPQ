using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 加好友请求被拒绝/同意 回调参数
    /// </summary>
    public class FriendAddReqRetArgs
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 类型 0/1 暂时分不清目前的用途
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 事件的详细文字描述（拒绝你的加好友请求/同意你的加好友请求）
        /// </summary>
        public string TypeStatus { get; set; }

        /// <summary>
        /// 目标QQ
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 是否同意
        /// </summary>
        /// <returns></returns>
        public bool isAgree()
        {
            return this.TypeStatus.Contains("同意");
        }
    }
}