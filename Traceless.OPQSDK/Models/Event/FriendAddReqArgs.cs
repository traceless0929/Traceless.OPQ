namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 好友申请
    /// </summary>
    public class FriendAddReqArgs
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 来源类型 2011空间 2020QQ搜索 2004群组 2005讨论组
        /// </summary>
        public int FromType { get; set; }

        /// <summary>
        /// 识别申请的ID吧？
        /// </summary>
        public long Field_9 { get; set; }

        /// <summary>
        /// 请求的文字描述（收到好友请求 内容我是QQ大冰来源来自QQ群）
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 来源群号 （来源 为 2004 时 请填群ID 其他情况为0）
        /// </summary>
        public long FromGroupId { get; set; }

        /// <summary>
        /// 来源群名
        /// </summary>
        public string FromGroupName { get; set; }

        /// <summary>
        /// Action1忽略2同意3拒绝
        /// </summary>
        public int Action { get; set; }

        /// <summary>
        /// 处理好友请求
        /// </summary>
        /// <param name="action">1忽略2同意3拒绝</param>
        public void DealReq(int action)
        {
            this.Action = action;
            Apis.DealFriend(this);
        }
    }
}