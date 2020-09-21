using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 群邀请事件回调参数
    /// </summary>
    public class GroupInviteArgs
    {
        /// <summary>
        /// 序列号
        /// </summary>
        public long Seq { get; set; }

        /// <summary>
        /// 类型 1入群 ActionUin=0表示有人邀请,=0表示申请入群 2加群申请被同意 5退群 ActionUin=0表示有人踢出,=0表示主动退群
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 邀请类型的文字描述（邀请加群/加群申请/加群申请已同意）
        /// </summary>
        public string MsgTypeStr { get; set; }

        /// <summary>
        /// 被邀请的人
        /// </summary>
        public long Who { get; set; }

        /// <summary>
        /// 被邀请人的昵称
        /// </summary>
        public string WhoName { get; set; }

        /// <summary>
        /// 消息类型状态
        /// </summary>
        public string MsgStatusStr { get; set; }

        /// <summary>
        /// 识别号？
        /// </summary>
        public int Flag_7 { get; set; }

        /// <summary>
        /// 识别号
        /// </summary>
        public int Flag_8 { get; set; }

        /// <summary>
        /// 群ID
        /// </summary>
        public long GroupId { get; set; }

        /// <summary>
        /// 群名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 邀请人(处理人)
        /// </summary>
        public int ActionUin { get; set; }

        /// <summary>
        /// 邀请人(处理人)昵称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 邀请人(处理人)群名片
        /// </summary>
        public string ActionGroupCard { get; set; }

        /// <summary>
        /// --11 同意 14 忽略 21 拒绝
        /// </summary>
        public int Action { get; set; }

        /// <summary>
        /// 处理群邀请
        /// </summary>
        /// <param name="action">11同意 14忽略 21拒绝</param>
        public void DealReq(int action)
        {
            this.Action = action;
            Apis.AnswerInviteGroup(this);
        }
    }
}