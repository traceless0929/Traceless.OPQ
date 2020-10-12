namespace Traceless.OPQSDK.Models.Api
{
    /// <summary>
    /// QQ相关ck
    /// </summary>
    public class QQCkResp
    {
        public string ClientKey { get; set; }
        public string Cookies { get; set; }
        public string Gtk { get; set; }
        public string Gtk32 { get; set; }
        public Pskey PSkey { get; set; }
        public string Skey { get; set; }
    }

    public class Pskey
    {
        public string connect { get; set; }
        public string docs { get; set; }
        public string docx { get; set; }
        public string game { get; set; }
        public string gamecenter { get; set; }
        public string imgcache { get; set; }
        public string mtencentcom { get; set; }
        public string mail { get; set; }
        public string mma { get; set; }
        public string now { get; set; }
        public string office { get; set; }
        public string openmobile { get; set; }
        public string qqweb { get; set; }
        public string qun { get; set; }
        public string qzone { get; set; }
        public string qzonecom { get; set; }
        public string tenpaycom { get; set; }
        public string ti { get; set; }
        public string vip { get; set; }
        public string weishi { get; set; }
    }
}