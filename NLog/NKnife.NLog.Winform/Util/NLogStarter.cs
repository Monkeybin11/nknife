using System;
using System.IO;
using NKnife.NLog.Winform.Properties;

namespace NKnife.NLog.Winform.Util
{
    class NLogStarter
    {
        public enum AppStyle
        {
            WinForm,
            Wpf
        }

        private const string ConfigFileName = "nlog.config";

        static NLogStarter()
        {
            Style = AppStyle.WinForm;
        }

        public static AppStyle Style { get; set; }

        public void Load()
        {
            //当发现程序目录中无NLog的配置文件时，根据程序的模式（WinForm或者WPF）自动释放不同NLog的配置文件
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);
            if (!File.Exists(file))
            {
                string configContent;
                switch (Style)
                {
                    case AppStyle.Wpf:
                        configContent = Resources.nlog_wpf_config;
                        break;
                    default:
                        configContent = Resources.nlog_winform_config;
                        break;
                }
                using (StreamWriter write = File.CreateText(file))
                {
                    write.Write(configContent);
                    write.Flush();
                    write.Dispose();
                }
            }

//            //配置Common.Logging适配器
//            var properties = new NameValueCollection();
//            properties["configType"] = "FILE";
//            properties["configFile"] = $"~/{ConfigFileName}";
//            LogManager.Adapter = new NLogLoggerFactoryAdapter(properties);
//
//
//            /****日志组件相关的IoC实例****/
//            switch (Style)
//            {
//                case AppStyle.Wpf:
//                {
//                    Bind<LoggerInfoDetailForm>().To<LoggerInfoDetailForm>().InSingletonScope();
//                    Bind<LogMessageFilter>().ToSelf().InSingletonScope();
//                    break;
//                }
//                case AppStyle.WinForm:
//                {
//                    Bind<LoggerInfoDetailForm>().ToSelf().InSingletonScope();
//                    Bind<LoggerCollectionViewModel>().ToSelf();
//                    Bind<CustomLogInfo>().ToSelf();
//                    break;
//                }
//            }
        }
    }
}