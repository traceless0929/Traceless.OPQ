using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.Utils.Ai.Tencent.Model
{
    /// <summary>
    /// 语音合成&gt;优图
    /// </summary>
    public class Aai_TtsResp
    {
        /// <summary>
        /// pcm:1 wav:2 map:3
        /// </summary>
        public int format { get; set; }

        /// <summary>
        /// 合成语音的base64编码数据
        /// </summary>
        public string speech { get; set; }

        /// <summary>
        /// 合成语音的md5摘要（base64编码之前）
        /// </summary>
        public string md5sum { get; set; }
    }
}