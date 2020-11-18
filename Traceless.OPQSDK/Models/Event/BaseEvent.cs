using System;
using Newtonsoft.Json;

namespace Traceless.OPQSDK.Models.Event
{
    /// <summary>
    /// 基础事件
    /// </summary>
    public class BaseEvent
    {
        /// <summary>
        /// 基础事件消息体
        /// </summary>
        public EventMsgBase EventMsg { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>

        public EventType EventName { get; set; }
    }

    /// <summary>
    /// 带事件参数的基础事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseEvent<T>
    {
        /// <summary>
        /// 事件参数
        /// </summary>
        public T EventData { get; set; }

        /// <summary>
        /// 基础事件消息体
        /// </summary>
        public EventMsgBase EventMsg { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public EventType EventName { get; set; }

        public BaseEvent<E> CloneObj<E>()
        {
            BaseEvent<E> res=new BaseEvent<E>();
            res.EventMsg = this.EventMsg;
            res.EventName = this.EventName;
            res.EventData = JsonConvert.DeserializeObject<E>(EventData.ToString()!);
            return res;

        }
    }
}