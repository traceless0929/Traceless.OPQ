using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Traceless.OPQSDK.Models;
using Traceless.OPQSDK.Models.Event;
using Traceless.OPQSDK.Models.Msg;
using Traceless.SocketIO;
using Traceless.SocketIO.Messages;

namespace Traceless.OPQSDK.Plugin
{
    public class OPQMain
    {
        public static Client socket;
        private static ConcurrentBag<BasePlugin> _instanceBag = new ConcurrentBag<BasePlugin>();

        public static async Task Client()
        {
            socket = new Client(System.Configuration.ConfigurationManager.AppSettings["address"]);
            socket.Opened += SocketOpened;
            socket.Message += SocketMessage;
            socket.SocketConnectionClosed += SocketConnectionClosed;
            socket.Error += SocketError;
            socket.Connect();

            foreach (var assembly in Assembly.GetEntryAssembly().GetTypes().Where(p => p.BaseType.IsAbstract && p.BaseType.Name == "BasePlugin")
                .OrderByDescending(p => ((BasePlugin)Activator.CreateInstance(p)).PluginPriority))
            {
                object obj = Activator.CreateInstance(assembly);
                BasePlugin basePlugin = (BasePlugin)obj;
                _instanceBag.Add(basePlugin);
                Console.WriteLine($"找到插件{assembly.Name}\n[{basePlugin.AppId}({basePlugin.pluginName})]\n优先级:{basePlugin.PluginPriority}\n作者：{basePlugin.pluginAuthor}\n描述：{basePlugin.PluginDescription}");
                Console.WriteLine($"-------------------------------------");
            }
            Console.WriteLine($"共{_instanceBag.Count}个");

            socket.On("connect", (fn) =>
            {
                Console.WriteLine(((ConnectMessage)fn).ConnectMsg);
                //重连成功 取得 在线QQ的websocket 链接connid
                //Ack
                socket.Emit("GetWebConn", System.Configuration.ConfigurationManager.AppSettings["robotqq"], null, async (callback) =>
                {
                    var jsonMsg = callback as string;
                    Console.WriteLine(string.Format("callback [root].[messageAck]: {0} \r\n", jsonMsg));
                    if (!jsonMsg.Contains("OK"))
                    {
                        //处理有些时候掉线收不到某些消息的问题，重新连接可以解决
                        socket.Close();
                        await Client();
                        return;
                    }
                    else
                    {
                        string masterQQ = System.Configuration.ConfigurationManager.AppSettings["masterqq"];
                        if (null == masterQQ || masterQQ.Length < 1)
                        {
                            Console.WriteLine(string.Format("没有配置主人QQ，不发送成功启动通知"));
                        }
                        else
                        {
                            Apis.SendFriendMsg(Convert.ToInt64(masterQQ), $"启动成功..");
                            foreach (var instance in _instanceBag)
                            {
                                instance.PluginInit(Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["robotqq"]));
                            }
                        }
                    }
                }
              );
            });

            //二维码检测事件
            socket.On("OnCheckLoginQrcode", (fn) =>
             {
                 Task.Run(() =>
                 {
                     Console.WriteLine("OnCheckLoginQrcode\n" + ((JSONMessage)fn).MessageText);
                 });
             });
            //收到群消息的回调事件
            socket.On("OnGroupMsgs", (fn) =>
            {
                Task.Run(() =>
                 {
                     BaseData<GroupMsg> baseData = JsonConvert.DeserializeObject<BaseData<GroupMsg>>(((JSONMessage)fn).MessageText);
                     if (baseData.CurrentPacket.Data.FromUserId == baseData.CurrentQQ)
                     {
                         //自己的消息不处理
                         return;
                     }
                     try
                     {
                         //拦截标记，0不拦截 1拦截
                         int stopFlag = 0;
                         foreach (var instance in _instanceBag)
                         {
                             stopFlag = instance.GroupMsgProcess(baseData.CurrentPacket.Data, baseData.CurrentQQ);
                             if (stopFlag > 0)
                             {
                                 break;
                             }
                         }
                     }
                     catch (Exception ex)
                     {
                         Console.WriteLine("[Error]" + ex.ToString());
                     }
                 });
            });
            //收到好友消息的回调事件
            socket.On("OnFriendMsgs", (fn) =>
            {
                Task.Run(() =>
                    {
                        BaseData<FriendMsg> baseData = JsonConvert.DeserializeObject<BaseData<FriendMsg>>(((JSONMessage)fn).MessageText);
                        if (baseData.CurrentPacket.Data.FromUin == baseData.CurrentQQ)
                        {
                            //自己的消息不处理
                            return;
                        }
                        try
                        {
                            //拦截标记，0不拦截 1拦截
                            int stopFlag = 0;
                            foreach (var instance in _instanceBag)
                            {
                                stopFlag = instance.FriendMsgProcess(baseData.CurrentPacket.Data, baseData.CurrentQQ);
                                if (stopFlag > 0)
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("[Error]" + ex.ToString());
                        }
                    });
            });

            //统一事件管理如好友进群事件 好友请求事件 退群等事件集合
            socket.On("OnEvents", (fn) =>
            {
                Task.Run(() =>
                    {
                        string msgText = ((JSONMessage)fn).MessageText;
                        Console.WriteLine("OnEnevts\n" + msgText);
                        BaseData<BaseEvent<object>> baseData = JsonConvert.DeserializeObject<BaseData<BaseEvent<object>>>(msgText);
                        try
                        {
                            foreach (var instance in _instanceBag)
                            {
                                switch (baseData.CurrentPacket.Data.EventMsg.MsgType)
                                {
                                    case EventType.ON_EVENT_QQ_LOGIN_SUCC:
                                        instance.EventQQLogin(JsonConvert.DeserializeObject<BaseData<BaseEvent<QNetArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                                        break;

                                    case EventType.ON_EVENT_QQ_OFFLINE:
                                        instance.EventQQOffline(JsonConvert.DeserializeObject<BaseData<BaseEvent<QNetArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                                        break;

                                    case EventType.ON_EVENT_QQ_NETWORK_CHANGE:
                                        instance.EventFramNetChange(JsonConvert.DeserializeObject<BaseData<BaseEvent<QNetArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_FRIEND_ADD_STATUS:
                                        instance.EventQQFriendAddRet(JsonConvert.DeserializeObject<BaseData<BaseEvent<FriendAddReqRetArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_NOTIFY_PUSHADDFRD:
                                        instance.EventQQFriendAddPush(JsonConvert.DeserializeObject<BaseData<BaseEvent<FriendAddPushArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_FRIEND_ADDED:
                                        instance.EventQQFriendAddReq(JsonConvert.DeserializeObject<BaseData<BaseEvent<FriendAddReqArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_GROUP_EXIT_SUCC:
                                        instance.EventQQGroupExitSuc(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupExitSucArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_FRIEND_REVOKE:
                                        instance.EventQQFriendRevoke(JsonConvert.DeserializeObject<BaseData<BaseEvent<FriendRevokeArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_GROUP_SHUT:
                                        instance.EventQQGroupShut(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupShutArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_FRIEND_DELETE:
                                        instance.EventQQFriendDelete(JsonConvert.DeserializeObject<BaseData<BaseEvent<FriendDeletArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_GROUP_REVOKE:
                                        instance.EventQQGroupRevoke(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupRevokeArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_GROUP_UNIQUETITTLE_CHANGED:
                                        instance.EventQQGroupTitleChange(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupTitleChangeArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_GROUP_JOIN:
                                        instance.EventQQGroupJoin(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupJoinReqArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_GROUP_ADMIN:
                                        instance.EventQQGroupAdminChange(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupAdminChangeArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_GROUP_EXIT:
                                        instance.EventQQGroupExitPush(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupExitPushArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_GROUP_JOIN_SUCC:
                                        instance.EventQQGroupJoinSuc(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupJoinSucArgs>>>(msgText).CurrentPacket.Data,
                                baseData.CurrentQQ); break;

                                    case EventType.ON_EVENT_GROUP_ADMINSYSNOTIFY:
                                        instance.EventQQGroupInvite(JsonConvert.DeserializeObject<BaseData<BaseEvent<GroupInviteArgs>>>(msgText).CurrentPacket.Data, baseData.CurrentQQ);
                                        break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("[Error]" + ex.ToString());
                        }
                    });
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