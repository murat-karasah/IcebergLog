namespace IcebergLog.Core.Config
{
    public class LogFilter
    {
        public static bool ShouldLog(IcebergLog.Core.Models.LogLevel logLevel, IcebergLog.Core.Models.LogLevel minLevel)
        {
            return logLevel >= minLevel;
        }
    }
}
