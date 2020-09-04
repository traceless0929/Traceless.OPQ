using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Traceless.OPQSDK;
using Traceless.OPQSDK.Models.Content;
using Traceless.OPQSDK.Models.Event;
using Traceless.OPQSDK.Models.Msg;

namespace Traceless.Robot.Plugins
{
    public class MyPlugin : BasePlugin
    {
        public override string pluginName => "测试插件";

        public override string pluginAuthor => "Traceless";

        public override string AppId => "Traceless.Demo";

        public override string PluginDescription => "这是个demo";

        public override int PluginPriority => 9999;

        /// <summary>
        /// 群消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int GroupMsgProcess(GroupMsg msg, long CurrentQQ)
        {
            Console.WriteLine($"GroupMsgProcess {CurrentQQ}\n" + JsonConvert.SerializeObject(msg));
            if (msg.FromGroupId != 516141713) { return 0; }
            if (msg.MsgType == MsgType.PicMsg)
            {
                PicContent picContent = msg.GetPic();
                Apis.SendGroupMsg(msg.FromGroupId, picContent.Content, picContent.GroupPic.FirstOrDefault().Url);
            }
            else if (msg.MsgType == MsgType.VoiceMsg)
            {
                VoiceContent voiceContent = msg.GetVoice();
                Apis.SendGroupMsg(msg.FromGroupId, voiceContent.Content, "", voiceContent.Url);
            }
            else
            {
                Apis.SendGroupMsg(msg.FromGroupId, msg.Content);
            }
            Apis.RevokeMsg(new OPQSDK.Models.Api.RevokeMsgReq { GroupID = msg.FromGroupId, MsgRandom = msg.MsgRandom, MsgSeq = msg.MsgRandom });
            return 0;
        }

        /// <summary>
        /// 私聊消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int FriendMsgProcess(FriendMsg msg, long CurrentQQ)
        {
            Console.WriteLine($"FriendMsgProcess {CurrentQQ}\n" + JsonConvert.SerializeObject(msg));
            if (msg.MsgType == MsgType.PicMsg)
            {
                PicContent picContent = msg.GetPic();
                Apis.SendFriendMsg(msg.FromUin, picContent.Content, picContent.FriendPic.FirstOrDefault().Url);
            }
            else if (msg.MsgType == MsgType.VoiceMsg)
            {
                VoiceContent voiceContent = msg.GetVoice();
                Apis.SendFriendMsg(msg.FromUin, voiceContent.Content, "", voiceContent.Url);
            }
            else
            {
                Apis.SendFriendMsg(msg.FromUin, msg.Content);
            }

            return 0;
        }

        /// <summary>
        /// QQ登陆成功事件
        /// </summary>
        /// <param name="msg"></param>
        public static void EventQQLogin(BaseEvent<QNetArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQLogin {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 网络变化事件 网络波动引起当前链接 释放 随机8-15s会自动重连登陆 被t下线的QQ 不会在重连
        /// </summary>
        /// <param name="msg"></param>
        public static void EventFramNetChange(BaseEvent<QNetArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQNetChange {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// QQ离线事件 可能的原因(TX 踢号/异地登陆/冻结/被举报等 导致等Session失效)
        /// </summary>
        /// <param name="msg"></param>
        public static void EventQQOffline(BaseEvent<QNetArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQOffline {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 加好友申请被同意/拒绝
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQFriendAddRet(BaseEvent<FriendAddReqRetArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQFriendAddRet {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 主动删除了好友
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQFriendDelete(BaseEvent<FriendDeletArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQFriendDelete {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 加好友成功后的通知
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQFriendAddPush(BaseEvent<FriendAddPushArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQFriendAddPush {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 收到好友请求
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQFriendAddReq(BaseEvent<FriendAddReqArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQFriendAddReq {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 退群成功
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQGroupExitSuc(BaseEvent<GroupExitSucArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQGroupExitSuc {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 好友消息撤回
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQFriendRevoke(BaseEvent<FriendRevokeArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQFriendRevoke {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 群禁言
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQGroupShut(BaseEvent<GroupShutArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQGroupShut {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 群撤回
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQGroupRevoke(BaseEvent<GroupRevokeArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQGroupRevoke {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 群头衔变更
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQGroupTitleChange(BaseEvent<GroupTitleChangeArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQGroupTitleChange {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 加群请求
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQGroupJoin(BaseEvent<GroupJoinReqArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQGroupJoin {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 群管理变更-机器人是不是管理员都能收到此群管变更事件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQGroupAdminChange(BaseEvent<GroupAdminChangeArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQGroupAdminChange {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 有人退群-无论机器人是不是管理员 群里任意成员 都能收到 此退群事件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQGroupExitPush(BaseEvent<GroupExitPushArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQGroupExitPush {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 加群成功
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQGroupJoinSuc(BaseEvent<GroupJoinSucArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQGroupJoinSuc {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }

        /// <summary>
        /// 收到群邀请
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentQQ"></param>
        public static void EventQQGroupInvite(BaseEvent<GroupInviteArgs> msg, long currentQQ)
        {
            Console.WriteLine($"EventQQGroupInvite {currentQQ}\n" + JsonConvert.SerializeObject(msg));
        }
    }
}