using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Traceless.Utils
{
    public class FileUtils
    {
        /// <summary>
        /// Base64字符串转文件并保存
        /// </summary>
        /// <param name="base64String">base64字符串</param>
        /// <param name="fileName">保存的文件名</param>
        /// <returns>是否转换并保存成功</returns>
        public static bool Base64StringToFile(string base64String, string fileName)
        {
            bool opResult = false;
            try
            {
                string strDate = TimeStamp.ConvertToTimeStamp(DateTime.Now) + "";
                string fileFullPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), strDate);//文件保存路径
                if (!Directory.Exists(fileFullPath))
                {
                    Directory.CreateDirectory(fileFullPath);
                }

                string strbase64 = base64String.Trim().Substring(base64String.IndexOf(",") + 1);   //将‘，’以前的多余字符串删除
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(strbase64));
                FileStream fs = new FileStream(fileFullPath + "\\" + fileName, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] b = stream.ToArray();
                fs.Write(b, 0, b.Length);
                fs.Close();

                opResult = true;
            }
            catch (Exception e)
            {
            }
            return opResult;
        }

        /// <summary>
        /// 文件转Base64
        /// </summary>
        /// <param name="filePath">完整文件路径</param>
        /// <returns></returns>
        public static string FileToBase64String(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            string base64Str = "";
            try
            {
                //读写指针移到距开头10个字节处
                fs.Seek(0, SeekOrigin.Begin);
                byte[] bs = new byte[fs.Length];
                int log = Convert.ToInt32(fs.Length);
                //从文件中读取10个字节放到数组bs中
                fs.Read(bs, 0, log);
                base64Str = Convert.ToBase64String(bs);
                return base64Str;
            }
            catch (Exception ex)
            {
                return base64Str;
            }
            finally
            {
                fs.Close();
            }
        }
    }
}