using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Traceless.OPQSDK.Models;
using Traceless.OPQSDK.Models.Event;
using Traceless.OPQSDK.Models.Msg;
using Traceless.Robot.Plugins;
using Traceless.SocketIO;
using Traceless.SocketIO.Messages;

namespace Traceless.Robot
{
    public class Program
    {
        public static Client socket;

        private static async Task Main(string[] args)
        {
            await Client();
            while (true)
            {
                string temp = Console.ReadLine();
            }
        }

        public static async Task Client()
        {
            socket = new Client(System.Configuration.ConfigurationManager.AppSettings["address"]);
            socket.Opened += SocketOpened;
            socket.Message += SocketMessage;
            socket.SocketConnectionClosed += SocketConnectionClosed;
            socket.Error += SocketError;
            socket.Connect();

            socket.On("connect", (fn) =>
            {
                Console.WriteLine(((ConnectMessage)fn).ConnectMsg);
                //重连成功 取得 在线QQ的websocket 链接connid
                //Ack
                socket.Emit("GetWebConn", System.Configuration.ConfigurationManager.AppSettings["robotqq"], null, (callback) =>
                {
                    var jsonMsg = callback as string;
                    Console.WriteLine(string.Format("callback [root].[messageAck]: {0} \r\n", jsonMsg));
                    if (!jsonMsg.Contains("OK"))
                    {
                        //处理有些时候掉线收不到某些消息的问题，重新连接可以解决
                        socket.Close();
                        Client();
                        return;
                    }
                }
              );
            });

            //二维码检测事件
            socket.On("OnCheckLoginQrcode", (fn) =>
            {
                Console.WriteLine("OnCheckLoginQrcode\n" + ((JSONMessage)fn).MessageText);
            });
            //收到群消息的回调事件
            socket.On("OnGroupMsgs", (fn) =>
            {
                BaseData<GroupMsg> baseData = JsonConvert.DeserializeObject<BaseData<GroupMsg>>(((JSONMessage)fn).MessageText);
                if (baseData.CurrentPacket.Data.FromUserId == baseData.CurrentQQ)
                {
                    //自己的消息不处理
                    return;
                }
                try
                {
                    BasePlugin.GroupMsgProcess(baseData.CurrentPacket.Data, baseData.CurrentQQ);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Error]" + ex.ToString());
                }
            });
            //收到好友消息的回调事件
            socket.On("OnFriendMsgs", (fn) =>
            {
                BaseData<FriendMsg> baseData = JsonConvert.DeserializeObject<BaseData<FriendMsg>>(((JSONMessage)fn).MessageText);
                if (baseData.CurrentPacket.Data.FromUin == baseData.CurrentQQ)
                {
                    //自己的消息不处理
                    return;
                }
                try
                {
                    BasePlugin.FriendMsgProcess(baseData.CurrentPacket.Data, baseData.CurrentQQ);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Error]" + ex.ToString());
                }
            });

            //统一事件管理如好友进群事件 好友请求事件 退群等事件集合
            socket.On("OnEvents", (fn) =>
            {
                string msgText = ((JSONMessage)fn).MessageText;
                Console.WriteLine("OnEnevts\n" + msgText);
                BaseData<BaseEvent> baseData = JsonConvert.DeserializeObject<BaseData<BaseEvent>>(msgText);
                try
                {
                    switch (baseData.CurrentPacket.Data.EventMsg.MsgType)
                    {
                        case EventType.ON_EVENT_QQ_LOGIN_SUCC:
                            BasePlugin.EventQQLogin(JsonConvert.DeserializeObject<BaseData<BaseEvent<QNetArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_QQ_OFFLINE:
                            BasePlugin.EventQQOffline(JsonConvert.DeserializeObject<BaseData<BaseEvent<QNetArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_QQ_NETWORK_CHANGE:
                            BasePlugin.EventFramNetChange(JsonConvert.DeserializeObject<BaseData<BaseEvent<QNetArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_FRIEND_ADD_STATUS:
                            BasePlugin.EventQQFriendAddRet(JsonConvert.DeserializeObject<BaseData<BaseEvent<FriendAddReqRetArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_NOTIFY_PUSHADDFRD:
                            BasePlugin.EventQQFriendAddPush(JsonConvert.DeserializeObject<BaseData<BaseEvent<FriendAddPushArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_FRIEND_ADDED:
                            BasePlugin.EventQQFriendAddReq(JsonConvert.DeserializeObject<BaseData<BaseEvent<FriendAddReqArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_GROUP_EXIT_SUCC:
                            BasePlugin.EventQQGroupExitSuc(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupExitSucArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_FRIEND_REVOKE:
                            BasePlugin.EventQQFriendRevoke(JsonConvert.DeserializeObject<BaseData<BaseEvent<FriendRevokeArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_GROUP_SHUT:
                            BasePlugin.EventQQGroupShut(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupShutArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_FRIEND_DELETE:
                            BasePlugin.EventQQFriendDelete(JsonConvert.DeserializeObject<BaseData<BaseEvent<FriendDeletArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_GROUP_REVOKE:
                            BasePlugin.EventQQGroupRevoke(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupRevokeArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_GROUP_UNIQUETITTLE_CHANGED:
                            BasePlugin.EventQQGroupTitleChange(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupTitleChangeArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_GROUP_JOIN:
                            BasePlugin.EventQQGroupJoin(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupJoinReqArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_GROUP_ADMIN:
                            BasePlugin.EventQQGroupAdminChange(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupAdminChangeArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_GROUP_EXIT:
                            BasePlugin.EventQQGroupExitPush(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupExitPushArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_GROUP_JOIN_SUCC:
                            BasePlugin.EventQQGroupJoinSuc(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupJoinSucArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;

                        case EventType.ON_EVENT_GROUP_ADMINSYSNOTIFY:
                            BasePlugin.EventQQGroupInvite(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupInviteArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Error]" + ex.ToString());
                }
            });
        }

        private static void SocketOpened(object sender, EventArgs e)
        {
            Console.WriteLine("SocketOpened\r\n");
        }

        private static void SocketError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("socket client error:");
            Console.WriteLine(e.Message);
        }

        private static void SocketConnectionClosed(object sender, EventArgs e)
        {
            Console.WriteLine("WebSocketConnection was terminated!");
        }

        private static void SocketMessage(object sender, MessageEventArgs e)
        {
            // uncomment to show any non-registered messages
            if (string.IsNullOrEmpty(e.Message.Event))
                Console.WriteLine("Generic SocketMessage: {0}", e.Message.MessageText);
            else
                Console.WriteLine("Generic SocketMessage: {0} : {1}", e.Message.Event, e.Message.Json.ToJsonString());
        }
    }
}