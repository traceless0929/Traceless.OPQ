namespace Traceless.OPQSDK.Models.Api
{
    /// <summary>
    /// 获取好友列表请求 返回json中 StartIndex == Friend_count 说明拉取好友列表完毕 否则 传入StartIndex 继续请求
    /// </summary>
    public class FriendListReq
    {
        /// <summary>
        /// 开始索引
        /// </summary>
        public long StartIndex { get; set; } = 0;
    }
}