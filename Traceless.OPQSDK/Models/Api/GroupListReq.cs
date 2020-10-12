namespace Traceless.OPQSDK.Models.Api
{
    /// <summary>
    /// 获取群列表请求 首次请求 {“NextToken”:""} 第二次请求NextToken 请填值 返回json 中 TroopList==null 时说明拉取群列表完成
    /// </summary>
    public class GroupListReq
    {
        /// <summary>
        /// 请求token
        /// </summary>
        public string NextToken { get; set; } = "";
    }
}