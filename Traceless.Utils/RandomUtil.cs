using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.Utils
{
    public class RandomUtil
    {
        private static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static int RandomGet(int min, int max)
        {
            long tick = DateTime.Now.Millisecond;
            Random rd = new Random(GetRandomSeed());
            int r = rd.Next(min, max);
            return r;
        }

        /// <summary>
        /// 随机产生结果(万分之CEN)
        /// </summary>
        /// <param name="cen">概率</param>
        /// <returns>true：中 false：不中</returns>
        public static bool RandomGet(int cen)
        {
            long tick = DateTime.Now.Millisecond;
            Random rd = new Random(GetRandomSeed());
            int r = rd.Next(0, 10000);
            if (cen < 0) return true;
            if (r < cen) return true;
            else return false;
        }
    }
}