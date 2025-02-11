namespace IcebergLog.Core.Models
{
    public class LogEntry
    {
        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public LogEntry(LogLevel level, string message, string source)
        {
            Level = level;
            Message = message;
            Source = source;
            Timestamp = DateTime.UtcNow;
        }

        public LogEntry() { } 
    }
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }
}
