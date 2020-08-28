using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.OPQSDK.Models.Content
{
    public class RepContent : AtContent
    {
        /// <summary>
        /// 回复引用中的标题
        /// </summary>
        public string ReplayContent { get; set; }

        /// <summary>
        /// 回复引用中的内容
        /// </summary>
        public string SrcContent { get; set; }
    }
}