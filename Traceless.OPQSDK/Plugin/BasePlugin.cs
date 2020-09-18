using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Traceless.OPQSDK;
using Traceless.OPQSDK.Models.Content;
using Traceless.OPQSDK.Models.Event;
using Traceless.OPQSDK.Models.Msg;

namespace Traceless.OPQSDK.Plugin
{
    /// <summary>
    /// 所有插件的基类
    /// </summary>
    public abstract class BasePlugin
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        public abstract string pluginName { get; }

        /// <summary>
        /// 插件作者
        /// </summary>
        public abstract string pluginAuthor { get; }

        /// <summary>
        /// 插件ID
        /// </summary>
        public abstract string AppId { get; }

        /// <summary>
        /// 插件描述
        /// </summary>
        public abstract string PluginDescription { get; }

        /// <summary>
        /// 优先级
        /// </summary>
        public abstract int PluginPriority { get; }

        /// <summary>
        /// 群消息
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        /// <returns>0不拦截 1拦截消息</returns>
        public abstract int GroupMsgProcess(GroupMsg msg, long currentQQ);

        /// <summary>
        /// 私聊消息
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        /// <returns>0不拦截 1拦截消息</returns>
        public abstract int FriendMsgProcess(FriendMsg msg, long currentQQ);

        /// <summary>
        /// QQ登陆成功事件
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        public abstract void EventQQLogin(BaseEvent<QNetArgs> msg, long currentQQ);

        /// <summary>
        /// 网络变化事件 网络波动引起当前链接 释放 随机8-15s会自动重连登陆 被t下线的QQ 不会在重连
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        public abstract void EventFramNetChange(BaseEvent<QNetArgs> msg, long currentQQ);

        /// <summary>
        /// QQ离线事件 可能的原因(TX 踢号/异地登陆/冻结/被举报等 导致等Session失效)
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        public abstract void EventQQOffline(BaseEvent<QNetArgs> msg, long currentQQ);

        /// <summary>
        /// 加好友申请被同意/拒绝
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="currentQQ"></param>
        public abstract void EventQQFriendAddRet(BaseEvent<FriendAddReqRetArgs> msg, long currentQQ);

        /// <summary>
        /// 主动删除了好友
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="currentQQ"></param>
        public abstract void EventQQFriendDelete(BaseEvent<FriendDeletArgs> msg, long currentQQ);

        /// <summary>
        /// 加好友成功后的通知
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="currentQQ"></param>
        public abstract void EventQQFriendAddPush(BaseEvent<FriendAddPushArgs> msg, long currentQQ);

        /// <summary>
        /// 收到好友请求
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        public abstract void EventQQFriendAddReq(BaseEvent<FriendAddReqArgs> msg, long currentQQ);

        /// <summary>
        /// 退群成功
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        public abstract void EventQQGroupExitSuc(BaseEvent<GroupExitSucArgs> msg, long currentQQ);

        /// <summary>
        /// 好友消息撤回
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        public abstract void EventQQFriendRevoke(BaseEvent<FriendRevokeArgs> msg, long currentQQ);

        /// <summary>
        /// 群禁言
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="currentQQ"></param>
        public abstract void EventQQGroupShut(BaseEvent<GroupShutArgs> msg, long currentQQ);

        /// <summary>
        /// 群撤回
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        public abstract void EventQQGroupRevoke(BaseEvent<GroupRevokeArgs> msg, long currentQQ);

        /// <summary>
        /// 群头衔变更
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        public abstract void EventQQGroupTitleChange(BaseEvent<GroupTitleChangeArgs> msg, long currentQQ);

        /// <summary>
        /// 加群相关，加群请求、成功入群
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="currentQQ"></param>
        public abstract void EventQQGroupJoin(BaseEvent<GroupJoinReqArgs> msg, long currentQQ);

        /// <summary>
        /// 群管理变更-机器人是不是管理员都能收到此群管变更事件
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        public abstract void EventQQGroupAdminChange(BaseEvent<GroupAdminChangeArgs> msg, long currentQQ);

        /// <summary>
        /// 有人退群-无论机器人是不是管理员 群里任意成员 都能收到 此退群事件
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        public abstract void EventQQGroupExitPush(BaseEvent<GroupExitPushArgs> msg, long currentQQ);

        /// <summary>
        /// 加群成功
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        public abstract void EventQQGroupJoinSuc(BaseEvent<GroupJoinSucArgs> msg, long currentQQ);

        /// <summary>
        /// 入群相关，被邀请、主动申请
        /// </summary>
        /// <param name="msg">消息体</param>
        /// <param name="currentQQ">当前机器人QQ</param>
        public abstract void EventQQGroupInvite(BaseEvent<GroupInviteArgs> msg, long currentQQ);

        /// <summary>
        /// 插件初始化
        /// </summary>
        /// <param name="currentQQ"></param>
        public abstract void PluginInit(long currentQQ);
    }
}