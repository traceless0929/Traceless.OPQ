using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.Utils.Ai.Tencent.Model
{
    public class BaseResp<T>
    {
        public int ret { get; set; }
        public string msg { get; set; }
        public T data { get; set; }
    }
}