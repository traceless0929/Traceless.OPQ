namespace Traceless.OPQSDK.Models.Event
{
    public enum EventType
    {
        /// <summary>
        /// QQ登陆成功事件
        /// </summary>
        ON_EVENT_QQ_LOGIN_SUCC,

        /// <summary>
        /// QQ离线事件 可能的原因(TX 踢号/异地登陆/冻结/被举报等 导致等Session失效)
        /// </summary>
        ON_EVENT_QQ_OFFLINE,

        /// <summary>
        /// 网络变化事件 网络波动引起当前链接 释放 随机8-15s会自动重连登陆 被t下线的QQ 不会在重连
        /// </summary>
        ON_EVENT_QQ_NETWORK_CHANGE,

        /// <summary>
        /// 添加好友的状态事件 被同意添加好友／被拒绝添加好友
        /// </summary>
        ON_EVENT_FRIEND_ADD_STATUS,

        /// <summary>
        /// 添加好友成功后的反馈
        /// </summary>
        ON_EVENT_NOTIFY_PUSHADDFRD,

        /// <summary>
        /// 收到好友请求事件 Action1忽略2同意3拒绝
        /// </summary>
        ON_EVENT_FRIEND_ADDED,

        /// <summary>
        /// 主动退出群聊
        /// </summary>
        ON_EVENT_GROUP_EXIT_SUCC,

        /// <summary>
        /// 好友撤回消息
        /// </summary>
        ON_EVENT_FRIEND_REVOKE,

        /// <summary>
        /// 禁言事件 UserID=0 全员禁言
        /// </summary>

        ON_EVENT_GROUP_SHUT,

        /// <summary>
        /// 删除好友事件
        /// </summary>
        ON_EVENT_FRIEND_DELETE,

        /// <summary>
        /// 群成员撤回消息
        /// </summary>
        ON_EVENT_GROUP_REVOKE,

        /// <summary>
        /// 群成员头衔变更事件
        /// </summary>

        ON_EVENT_GROUP_UNIQUETITTLE_CHANGED,

        /// <summary>
        /// 群成员加入事件
        /// </summary>
        ON_EVENT_GROUP_JOIN,

        /// <summary>
        /// 群管理员变更（机器人是不是管理员都能收到此群管变更事件）
        /// </summary>
        ON_EVENT_GROUP_ADMIN,

        /// <summary>
        /// 群成员退出事件（无论机器人是不是管理员 群里任意成员 都能收到 此退群事件）
        /// </summary>
        ON_EVENT_GROUP_EXIT,

        /// <summary>
        /// 主动进群成功事件
        /// </summary>
        ON_EVENT_GROUP_JOIN_SUCC,

        /// <summary>
        /// 群管理系统消息 申请加群 邀请加群等通知
        /// </summary>
        ON_EVENT_GROUP_ADMINSYSNOTIFY
    }
}