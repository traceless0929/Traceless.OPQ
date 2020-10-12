namespace Traceless.OPQSDK.Models.Api
{
    /// <summary>
    /// 搜索群组请求
    /// </summary>
    public class GroupSearchReq
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int Page { get; set; } = 0;

        /// <summary>
        /// 关键字
        /// </summary>
        public string Content { get; set; } = "关键字";
    }
}