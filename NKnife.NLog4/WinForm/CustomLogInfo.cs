using System;
using NLog;

namespace NKnife.NLog4.WinForm
{
    public class CustomLogInfo
    {
        public CustomLogInfo()
        {
        }

        public CustomLogInfo(LogEventInfo logInfo)
        {
            LogInfo = logInfo;
        }

        public DateTime DateTime => LogInfo.TimeStamp;
        public LogEventInfo LogInfo { get; set; }
        public string Source => LogInfo.LoggerName;
        public LogLevel LogLevel => LogInfo.Level;
    }
}
