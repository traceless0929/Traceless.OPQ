using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Api
{
    /// <summary>
    /// 获取好友列表返回
    /// </summary>
    public class FriendListResp
    {
        /// <summary>
        /// 好友数量
        /// </summary>
        public int Friend_count { get; set; }

        /// <summary>
        /// 好友列表
        /// </summary>
        public Friendlist[] Friendlist { get; set; }

        /// <summary>
        /// 本次请求获取的好友数量上限
        /// </summary>
        public int GetfriendCount { get; set; }

        /// <summary>
        /// 开始索引
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// 总计好友数量
        /// </summary>
        public int Totoal_friend_count { get; set; }
    }

    public class Friendlist
    {
        /// <summary>
        /// 好友QQ
        /// </summary>
        public long FriendUin { get; set; }

        /// <summary>
        /// 是否备注
        /// </summary>
        public bool IsRemark { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 早下名字叫爱美高涛
        /// </summary>
        public string OnlineStr { get; set; }

        /// <summary>
        /// 备注内容
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态 不太懂干嘛用
        /// </summary>
        public int Status { get; set; }
    }
}