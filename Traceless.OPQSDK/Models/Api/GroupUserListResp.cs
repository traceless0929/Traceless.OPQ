using System.Collections.Generic;

namespace Traceless.OPQSDK.Models.Api
{
    /// <summary>
    /// 群成员列表
    /// </summary>
    public class GroupUserListResp
    {
        /// <summary>
        /// 本页数量
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        /// 群号
        /// </summary>
        public long GroupUin { get; set; }

        /// <summary>
        /// 本次页最后一个QQ号，为0表示最后一页，非零表示还有下一页，请求中LastUin填这个继续请求
        /// </summary>
        public long LastUin { get; set; }

        /// <summary>
        /// 群成员列表
        /// </summary>
        public List<GMemberInfo> MemberList { get; set; } = new List<GMemberInfo>();
    }

    /// <summary>
    /// 群成员列表项
    /// </summary>
    public class GMemberInfo
    {
        /// <summary>
        /// 年龄
        /// </summary>
        public long Age { get; set; }

        /// <summary>
        /// 自动备注，当没有群名片时，该值为QQ昵称，否则为空字符串
        /// </summary>
        public string AutoRemark { get; set; }

        /// <summary>
        /// 信用等级？
        /// </summary>
        public long CreditLevel { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 未知
        /// </summary>
        public long FaceId { get; set; }

        /// <summary>
        /// 0男 1女 255未知
        /// </summary>
        public long Gender { get; set; }

        /// <summary>
        /// 群管理权限 192群主 1管理 0成员
        /// </summary>
        public long GroupAdmin { get; set; }

        /// <summary>
        /// 群名片
        /// </summary>
        public string GroupCard { get; set; }

        /// <summary>
        /// 加入时间 秒戳
        /// </summary>
        public long JoinTime { get; set; }

        /// <summary>
        /// 最后发言时间
        /// </summary>
        public long LastSpeakTime { get; set; }

        /// <summary>
        /// 群成员等级【大概是头衔等级】
        /// </summary>
        public long MemberLevel { get; set; }

        /// <summary>
        /// QQ号
        /// </summary>
        public long MemberUin { get; set; }

        /// <summary>
        /// 啥东西？
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 啥东西？
        /// </summary>
        public string ShowName { get; set; }

        /// <summary>
        /// 专属头衔
        /// </summary>
        public string SpecialTitle { get; set; }

        /// <summary>
        /// 啥东西？
        /// </summary>
        public long Status { get; set; }
    }
}