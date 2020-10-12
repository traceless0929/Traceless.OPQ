namespace Traceless.OPQSDK.Models.Api
{
    public class SetGroupTitleReq
    {
        /// <summary>
        /// 群号
        /// </summary>
        public long GroupID { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 新头衔
        /// </summary>
        public string NewTitle { get; set; }
    }
}