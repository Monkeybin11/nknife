using System;
using System.IO;
using NKnife.NLog.WinForm.Properties;

namespace NKnife.NLog.WinForm
{
    public class NLogConfigSimpleFileCreate
    {
        private const string ConfigFileName = "nlog.config";

        public void Load()
        {
            //当发现程序目录中无NLog的配置文件时，自动释放NLog的基础配置文件
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);
            if (!File.Exists(file))
            {
                string configContent = Resources.nlog_winform_config;
                using (StreamWriter write = File.CreateText(file))
                {
                    write.Write(configContent);
                    write.Flush();
                    write.Dispose();
                }
            }
        }
    }
}