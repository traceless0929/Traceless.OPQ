namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// QQ登录\离线\网络波动 事件参数
    /// </summary>
    public class QNetArgs
    {
        public long QQUser { get; set; }
        public string Data { get; set; }
    }
}