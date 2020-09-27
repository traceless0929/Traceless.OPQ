using System;
using System.Collections.Generic;
using System.Text;

namespace Traceless.Utils.IniConfig
{
    public class IniManager
    {
        private string _rootPath = "";
        private Dictionary<string, IniConfig> _dataDics = new Dictionary<string, IniConfig>();

        public IniManager(string rootPath)
        {
            _rootPath = rootPath;
        }

        /// <summary>
        /// 获取某个配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IniConfig this[string key]
        {
            get
            {
                bool ok = _dataDics.TryGetValue(key, out IniConfig v);
                if (ok)
                {
                    return v;
                }
                return null;
            }
        }

        /// <summary>
        /// 增加配置
        /// </summary>
        /// <param name="iniName">文件名</param>
        /// <param name="iniConfig">配置文件</param>
        public void Add(string iniName, IniConfig iniConfig)
        {
            iniConfig.Save();
            _dataDics[iniName] = iniConfig;
        }

        /// <summary>
        /// 增加配置
        /// </summary>
        /// <param name="iniName"></param>
        /// <param name="func"></param>
        public void Add(string iniName, Func<IniConfig> func)
        {
            IniConfig iniConfig = func.Invoke();
            _dataDics[iniName] = iniConfig;
        }

        /// <summary>
        /// 获取某个配置
        /// </summary>
        /// <param name="iniName"></param>
        /// <returns></returns>
        public IniConfig Get(string iniName)
        {
            bool ok = _dataDics.TryGetValue(iniName, out IniConfig v);
            if (ok)
            {
                return v;
            }
            return null;
        }
    }
}