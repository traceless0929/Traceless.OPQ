using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Traceless.OPQSDK.Models.Api;
using Traceless.OPQSDK.Models.Content.Card.Json;
using Traceless.OPQSDK.Models.Event;
using Traceless.OPQSDK.Models.Msg;
using Traceless.OPQSDK.Plugin;
using Traceless.Utils;

namespace Traceless.OPQSDK
{
    /// <summary>
    /// 提供了所有Api方法
    /// </summary>
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
        /// 获取插件数据根目录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="plugin"></param>
        /// <returns></returns>
        public static string GetPluginDataDic<T>(T plugin) where T : BasePlugin
        {
            return System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), plugin.AppId);
        }

        public static string GetPluginDataDic(string appId)
        {
            return System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), appId);
        }

        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="groupId">群号</param>
        /// <param name="txt">文字内容</param>
        /// <param name="voice">语音消息【http开头的网络地址，或base64内容】</param>
        /// <param name="pic">图片消息【http开头的网络地址，或base64内容】</param>
        /// <param name="changeCode">是否转换OPQ吗</param>
        /// <returns></returns>
        public static MsgResp SendGroupMsg(long groupId, string txt = "", object json = null, bool changeCode = true)
        {
            if (string.IsNullOrEmpty(txt) && null == json)
            {
                return new MsgResp() { Ret = 1, Msg = "消息为空" };
            }
            SendMsgReq req = new SendMsgReq() { ToUserUid = groupId, SendMsgType = "TextMsg", SendToType = 2, Content = (txt == null ? "" : txt) };
            if (null != json)
            {
                req.SendMsgType = "JsonMsg";
                req.Content = JsonConvert.SerializeObject(json);
            }
            return SendMsg(req, changeCode);
        }

        /// <summary>
        /// 发送好友消息
        /// </summary>
        /// <param name="qq">好友QQ</param>
        /// <param name="txt">文字内容</param>
        /// <param name="voice">语音消息【http开头的网络地址，或base64内容】</param>
        /// <param name="pic">图片消息【http开头的网络地址，或base64内容】</param>
        /// <returns></returns>
        public static MsgResp SendFriendMsg(long qq, string txt = "", object json = null, bool changeCode = true)
        {
            if (string.IsNullOrEmpty(txt) && null == json)
            {
                return new MsgResp() { Ret = 1, Msg = "消息为空" };
            }
            SendMsgReq req = new SendMsgReq() { ToUserUid = qq, SendMsgType = "TextMsg", SendToType = 1, Content = (txt == null ? "" : txt) };
            if (null != json)
            {
                req.SendMsgType = "JsonMsg";
                req.Content = JsonConvert.SerializeObject(json);
            }
            return SendMsg(req, changeCode);
        }

        /// <summary>
        /// 群组管理：加群 拉人 踢群 退群
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static object GroupMgr(GroupMgrReq req)
        {
            return HttpUtils.Post<object>(_ApiAddress + "&funcname=GroupMgr", req);
        }

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static object AddQQUser(AddQQReq req)
        {
            return HttpUtils.Post<object>(_ApiAddress + "&funcname=AddQQUser", req);
        }

        /// <summary>
        /// 获取好友列表【建议缓存结果，过一段时间再调用】
        /// </summary>
        /// <returns></returns>
        public static List<Friendlist> GetQQFriendList()
        {
            List<Friendlist> res = new List<Friendlist>();
            FriendListReq req = new FriendListReq();
            FriendListResp friend = new FriendListResp();
            do
            {
                friend = HttpUtils.Post<FriendListResp>(_ApiAddress + "&funcname=friendlist.GetFriendListReq", req);
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
        public static List<GroupInfo> GetGroupList()
        {
            GroupListReq req = new GroupListReq();
            List<GroupInfo> res = new List<GroupInfo>();
            GroupListResp group = new GroupListResp();
            do
            {
                group = HttpUtils.Post<GroupListResp>(_ApiAddress + "&funcname=friendlist.GetTroopListReqV2", req);
                if (null == group)
                {
                    group = new GroupListResp();
                }
                if (group.TroopList != null && group.TroopList.Count > 0)
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
        public static List<GMemberInfo> GetGroupUserList(long gid)
        {
            GroupUserListReq req = new GroupUserListReq() { GroupUin = gid };
            List<GMemberInfo> res = new List<GMemberInfo>();
            GroupUserListResp group = new GroupUserListResp();
            do
            {
                group = HttpUtils.Post<GroupUserListResp>(_ApiAddress + "&funcname=friendlist.GetTroopMemberListReq", req);
                if (null == group)
                {
                    group = new GroupUserListResp();
                }
                if (group.MemberList.Count > 0)
                {
                    res.AddRange(group.MemberList);
                }
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
            return HttpUtils.Post<object>(_ApiAddress + "&funcname=PbMessageSvc.PbMsgWithDraw", req);
        }

        /// <summary>
        /// 搜索群组
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static List<GroupSearchItemResp> SearchGroup(GroupSearchReq req)
        {
            return HttpUtils.Post<List<GroupSearchItemResp>>(_ApiAddress + "&funcname=OidbSvc.0x8ba_31", req);
        }

        /// <summary>
        /// QQ资料卡点赞
        /// </summary>
        /// <returns></returns>
        public static object QQZan(long qq)
        {
            return HttpUtils.Post<object>(_ApiAddress + "&funcname=OidbSvc.0x7e5_4", new QQZanReq() { UserID = qq });
        }

        /// <summary>
        /// 获取QQ相关ck
        /// </summary>
        /// <returns></returns>
        public static QQCkResp GetQQCk()
        {
            return HttpUtils.Get<QQCkResp>(_ApiAddress + "&funcname=GetUserCook");
        }

        /// <summary>
        /// 处理好友请求
        /// </summary>
        /// <returns></returns>
        public static object DealFriend(FriendAddReqArgs arg)
        {
            return HttpUtils.Post<object>(_ApiAddress + "&funcname=DealFriend", arg);
        }

        /// <summary>
        /// 处理群邀请
        /// </summary>
        /// <returns></returns>
        public static object AnswerInviteGroup(GroupInviteArgs arg)
        {
            return HttpUtils.Post<object>(_ApiAddress + "&funcname=AnswerInviteGroup", arg);
        }

        /// <summary>
        /// 修改群名片
        /// </summary>
        /// <returns></returns>
        public static object ModifyGroupCard(SetGroupCardReq req)
        {
            return HttpUtils.Post<object>(_ApiAddress + "&funcname=friendlist.ModifyGroupCardReq", req);
        }

        /// <summary>
        /// 设置头衔
        /// </summary>
        /// <returns></returns>
        public static object SetUniqueTitle(SetGroupTitleReq req)
        {
            return HttpUtils.Post<object>(_ApiAddress + "&funcname=OidbSvc.0x8fc_2", req);
        }

        /// <summary>
        /// 发送公告
        /// </summary>
        /// <returns></returns>
        public static object Announce(SendGNoticeReq req)
        {
            return HttpUtils.Post<object>(_ApiAddress + "&funcname=Announce", req);
        }

        /// <summary>
        /// 全员禁言
        /// </summary>
        /// <returns></returns>
        public static object BanAllGroup(BanAllReq req)
        {
            return HttpUtils.Post<object>(_ApiAddress + "&funcname=OidbSvc.0x89a_0", req);
        }

        /// <summary>
        /// 禁言某人
        /// </summary>
        /// <returns></returns>
        public static object Ban(BanReq req)
        {
            return HttpUtils.Post<object>(_ApiAddress + "&funcname=OidbSvc.0x570_8", req);
        }

        /// <summary>
        /// 获取任意用户信息昵称头像等
        /// </summary>
        /// <param name="qq">目标用户QQ</param>
        /// <returns></returns>
        public static UserInfoResp GetUserInfo(long qq)
        {
            return HttpUtils.Post<UserInfoResp>(_ApiAddress + "&funcname=GetUserInfo", new QQZanReq() { UserID = qq });
        }

        /// <summary>
        /// 发消息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private static MsgResp SendMsg(SendMsgReq req, bool changeCode)
        {
            if (string.IsNullOrEmpty(req.Content))
            {
                return new MsgResp() { Ret = 1 };
            }
            Console.WriteLine($"[log]{(req.SendToType == 1 ? "好友" : (req.SendToType == 2 ? "群聊" : "私聊"))}给{req.ToUserUid}->{req.Content}");
            List<OPQCode> codes = OPQCode.Parse(req.Content);
            if (changeCode)
            {
                if (string.IsNullOrEmpty(req.PicUrl))
                {
                    codes.Where(p => p.Function == OPQFunction.Pic).ToList().ForEach(code =>
                      {
                          req.SendMsgType = "PicMsg";
                          req.PicUrl = code.Items.ContainsKey("url") ? code.Items["url"] : "";
                          req.PicPath = code.Items.ContainsKey("path") ? code.Items["path"] : "";
                          req.FlashPic = code.Items.ContainsKey("flash") ? false : ((code.Items["flash"] == (true + "") ? true : false));
                          req.Content = req.Content.Replace(code.ToString(), "");
                      });
                }
                if (string.IsNullOrEmpty(req.VoiceUrl))
                {
                    codes.Where(p => p.Function == OPQFunction.Voice).ToList().ForEach(code =>
                      {
                          req.SendMsgType = "VoiceMsg";
                          req.VoiceUrl = code.Items.ContainsKey("url") ? code.Items["url"] : "";
                          req.VoicePath = code.Items.ContainsKey("path") ? code.Items["path"] : "";
                          req.Content = req.Content.Replace(code.ToString(), "");
                      });
                }
                codes.Where(p => p.Function == OPQFunction.Rich).ToList().ForEach(code =>
                    {
                        req.SendMsgType = "JsonMsg";
                        req.Content = JsonConvert.SerializeObject(new RichCard(code.Items.GetValueOrDefault("title"), code.Items.GetValueOrDefault("desc"), code.Items.GetValueOrDefault("prompt"), code.Items.GetValueOrDefault("tag"), code.Items.GetValueOrDefault("url"), code.Items.GetValueOrDefault("preview")));
                    });
            }
            Console.WriteLine($"[DEBUG]{JsonConvert.SerializeObject(req)}");
            MsgResp msg = HttpUtils.Post<MsgResp>(_ApiAddress + "&funcname=SendMsgV2", req);
            int i = 0;
            while (msg.Ret == 241 && i < 10)
            {
                Console.WriteLine($"[WARN]API等待{JsonConvert.SerializeObject(req)}");
                Thread.Sleep(1100);
                msg = HttpUtils.Post<MsgResp>(_ApiAddress + "&funcname=SendMsgV2", req);
                i++;
            }
            if (msg.Ret == 241)
            {
                Console.WriteLine($"[WARN]API调用过于频繁，本条丢弃{JsonConvert.SerializeObject(req)}");
            }
            return msg;
        }
    }
}