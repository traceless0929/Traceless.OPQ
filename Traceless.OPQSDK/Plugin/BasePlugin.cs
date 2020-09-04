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
    }
}