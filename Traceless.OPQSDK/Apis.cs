using Newtonsoft.Json;
using Traceless.OPQSDK.Models.Api;
using Traceless.Utils.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Traceless.OPQSDK.Models.Event;
using Traceless.OPQSDK.Models;

namespace Traceless.OPQSDK
{
    public class Apis
    {
        private static string _ApiAddress = "";
        private static string _RobotQQ = "";

        static Apis()
        {
            _RobotQQ = System.Configuration.ConfigurationManager.AppSettings["robotqq"];
            _ApiAddress = System.Configuration.ConfigurationManager.AppSettings["address"] + "v1/LuaApiCaller?qq=" + _RobotQQ + "&timeout=10";
        }

        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="txt">文字内容</param>
        /// <param name="voice">语音消息【http开头的网络地址，或base64内容】</param>
        /// <param name="pic">图片消息【http开头的网络地址，或base64内容】</param>
        /// <returns></returns>
        public static MsgResp SendGroupMsg(long groupId, string txt = "", string pic = "", string voice = "")
        {
            if (string.IsNullOrEmpty(txt + voice + pic))
            {
                return new MsgResp() { Ret = 1, Msg = "消息为空" };
            }
            SendMsgReq req = new SendMsgReq() { toUser = groupId, sendMsgType = "TextMsg", sendToType = 2, content = (txt == null ? "" : txt) };
            if (!string.IsNullOrEmpty(voice))
            {
                req.content = "";
                req.sendMsgType = "VoiceMsg";
                req.voiceUrl = voice.StartsWith("http") ? voice : "";
                req.voiceBase64Buf = voice.StartsWith("http") ? "" : voice;
            }
            else if (!string.IsNullOrEmpty(pic))
            {
                req.sendMsgType = "PicMsg";
                req.picUrl = pic.StartsWith("http") ? pic : "";
                req.picBase64Buf = pic.StartsWith("http") ? "" : pic;
            }
            return SendMsg(req);
        }

        /// <summary>
        /// 发送好友消息
        /// </summary>
        /// <param name="qq">好友QQ</param>
        /// <param name="txt">文字内容</param>
        /// <param name="voice">语音消息【http开头的网络地址，或base64内容】</param>
        /// <param name="pic">图片消息【http开头的网络地址，或base64内容】</param>
        /// <returns></returns>
        public static MsgResp SendFriendMsg(long qq, string txt = "", string pic = "", string voice = "")
        {
            if (string.IsNullOrEmpty(txt + voice + pic))
            {
                return new MsgResp() { Ret = 1, Msg = "消息为空" };
            }
            SendMsgReq req = new SendMsgReq() { toUser = qq, sendMsgType = "TextMsg", sendToType = 1, content = (txt == null ? "" : txt) };
            if (!string.IsNullOrEmpty(voice))
            {
                req.content = "";
                req.sendMsgType = "VoiceMsg";
                req.voiceUrl = voice.StartsWith("http") ? voice : "";
                req.voiceBase64Buf = voice.StartsWith("http") ? "" : voice;
            }
            else if (!string.IsNullOrEmpty(pic))
            {
                req.sendMsgType = "PicMsg";
                req.picUrl = pic.StartsWith("http") ? pic : "";
                req.picBase64Buf = pic.StartsWith("http") ? "" : pic;
            }
            return SendMsg(req);
        }

        /// <summary>
        /// 群组管理：加群 拉人 踢群 退群
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static object GroupMgr(GroupMgrReq req)
        {
            return Post<object>(_ApiAddress + "&funcname=GroupMgr", req);
        }

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static object AddQQUser(AddQQReq req)
        {
            return Post<object>(_ApiAddress + "&funcname=AddQQUser", req);
        }

        /// <summary>
        /// 获取好友列表【建议缓存结果，过一段时间再调用】
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static List<Friendlist> GetQQFriendList()
        {
            List<Friendlist> res = new List<Friendlist>();
            FriendListReq req = new FriendListReq();
            FriendListResp friend = new FriendListResp();
            do
            {
                friend = Post<FriendListResp>(_ApiAddress + "&funcname=GetFriendListReq", req);
                res.AddRange(friend.Friendlist.GroupBy(p => p.FriendUin).Select(p => p.First()).ToList());
                req.StartIndex = friend.StartIndex;
            }
            while (friend.Totoal_friend_count != res.Count && friend.StartIndex != friend.Friend_count);
            //返回json中 StartIndex == Friend_count 说明拉取好友列表完毕 否则 传入StartIndex 继续请求
            return res;
        }

        /// <summary>
        /// 获取群组列表【建议缓存结果，过一段时间再调用】
        /// </summary>
        /// <returns></returns>
        public static List<Trooplist> GetGroupList()
        {
            GroupListReq req = new GroupListReq();
            List<Trooplist> res = new List<Trooplist>();
            GroupListResp group = new GroupListResp();
            do
            {
                group = Post<GroupListResp>(_ApiAddress + "&funcname=GetTroopListReqV2", req);
                if (group.TroopList != null)
                {
                    res.AddRange(group.TroopList.GroupBy(p => p.GroupId).Select(p => p.First()).ToList());
                }
                req.NextToken = group.NextToken;
            }
            while (group.NextToken.Length > 1 && group.TroopList != null);
            //首次请求 {“NextToken”:""} 第二次请求NextToken 请填值 返回json 中 TroopList==null 时说明拉取群列表完成
            return res;
        }

        /// <summary>
        /// 获群成员列表【建议缓存结果，过一段时间再调用】
        /// </summary>
        /// <returns></returns>
        public static List<Memberlist> GetGroupUserList(long gid)
        {
            GroupUserListReq req = new GroupUserListReq() { GroupUin = gid };
            List<Memberlist> res = new List<Memberlist>();
            GroupUserListResp group = new GroupUserListResp();
            do
            {
                group = Post<GroupUserListResp>(_ApiAddress + "&funcname=friendlist.GetTroopMemberListReq", req);
                res.AddRange(group.MemberList);
                req.LastUin = group.LastUin;
            }
            while (group.LastUin != 0);
            return res.GroupBy(p => p.MemberUin).Select(p => p.First()).ToList();
        }

        /// <summary>
        /// 撤回消息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static object RevokeMsg(RevokeMsgReq req)
        {
            return Post<object>(_ApiAddress + "&funcname=PbMessageSvc.PbMsgWithDraw", req);
        }

        /// <summary>
        /// 搜索群组
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static List<GroupSearchItemResp> SearchGroup(GroupSearchReq req)
        {
            return Post<List<GroupSearchItemResp>>(_ApiAddress + "&funcname=OidbSvc.0x8ba_31", req);
        }

        /// <summary>
        /// QQ资料卡点赞
        /// </summary>
        /// <returns></returns>
        public static object QQZan(long qq)
        {
            return Post<object>(_ApiAddress + "&funcname=OidbSvc.0x7e5_4", new QQZanReq() { UserID = qq });
        }

        /// <summary>
        /// 获取QQ相关ck
        /// </summary>
        /// <returns></returns>
        public static QQCkResp GetQQCk()
        {
            return Get<QQCkResp>(_ApiAddress + "&funcname=GetUserCook");
        }

        /// <summary>
        /// 处理好友请求
        /// </summary>
        /// <returns></returns>
        public static object DealFriend(FriendAddReqArgs arg)
        {
            return Post<object>(_ApiAddress + "&funcname=DealFriend", arg);
        }

        /// <summary>
        /// 处理群邀请
        /// </summary>
        /// <returns></returns>
        public static object AnswerInviteGroup(GroupInviteArgs arg)
        {
            return Post<object>(_ApiAddress + "&funcname=AnswerInviteGroup", arg);
        }

        /// <summary>
        /// 修改群名片
        /// </summary>
        /// <returns></returns>
        public static object ModifyGroupCard(SetGroupCardReq req)
        {
            return Post<object>(_ApiAddress + "&funcname=friendlist.ModifyGroupCardReq", req);
        }

        /// <summary>
        /// 设置头衔
        /// </summary>
        /// <returns></returns>
        public static object SetUniqueTitle(SetGroupTitleReq req)
        {
            return Post<object>(_ApiAddress + "&funcname=OidbSvc.0x8fc_2", req);
        }

        /// <summary>
        /// 发送公告
        /// </summary>
        /// <returns></returns>
        public static object Announce(SendGNoticeReq req)
        {
            return Post<object>(_ApiAddress + "&funcname=Announce", req);
        }

        /// <summary>
        /// 全员禁言
        /// </summary>
        /// <returns></returns>
        public static object BanAllGroup(BanAllReq req)
        {
            return Post<object>(_ApiAddress + "&funcname=OidbSvc.0x89a_0", req);
        }

        /// <summary>
        /// 禁言某人
        /// </summary>
        /// <returns></returns>
        public static object Ban(BanReq req)
        {
            return Post<object>(_ApiAddress + "&funcname=OidbSvc.0x570_8", req);
        }

        /// <summary>
        /// 获取任意用户信息昵称头像等
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static UserInfoResp GetUserInfo(long qq)
        {
            return Post<UserInfoResp>(_ApiAddress + "&funcname=GetUserInfo", new QQZanReq() { UserID = qq });
        }

        /// <summary>
        /// 发消息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private static MsgResp SendMsg(SendMsgReq req)
        {
            MsgResp msg = Post<MsgResp>(_ApiAddress + "&funcname=SendMsg", req);
            int i=0;
            while(msg.Ret==241&&i<10){
                System.Threading.Thread.Sleep(1100);
                msg = Post<MsgResp>(_ApiAddress + "&funcname=SendMsg", req);
                i++;
            }
            if(msg.Ret==241){
                
                Console.WriteLine($"[WARN]API调用过于频繁，本条丢弃{JsonConvert.SerializeObject(req)}");
            }
            return msg;
        }

        public static T Post<T>(string url, object data) where T : class
        {
            Task<HttpResponseMessage> responseMessage = PostAsync(url, JsonConvert.SerializeObject(data));
            if (responseMessage.Result.IsSuccessStatusCode)
            {
                Task<string> t = responseMessage.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(t.Result);
            }
            return default(T);
        }

        public static T Get<T>(string url) where T : class
        {
            Task<HttpResponseMessage> responseMessage = GetAsync(url);
            if (responseMessage.Result.IsSuccessStatusCode)
            {
                Task<string> t = responseMessage.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(t.Result);
            }
            return default(T);
        }

        private async static Task<HttpResponseMessage> PostAsync(string url, string postStr)
        {
            ITrHttpClientFactory factory = new TrHttpClientFactory();
            var client = factory.CreateHttpClient();
            return await client.PostAsync(url, postStr);
        }

        private async static Task<HttpResponseMessage> GetAsync(string url)
        {
            ITrHttpClientFactory factory = new TrHttpClientFactory();
            var client = factory.CreateHttpClient();
            return await client.GetAsync(url);
        }
    }
}