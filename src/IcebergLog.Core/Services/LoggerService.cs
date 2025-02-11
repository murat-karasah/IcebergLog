using IcebergLog.Core.Interfaces;
using IcebergLog.Core.Models;
using Serilog;
using Serilog.Events;

namespace IcebergLog.Core.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly Serilog.ILogger _logger; 

        public LoggerService()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/logs.txt", rollingInterval: Serilog.RollingInterval.Day) 
                .CreateLogger();
        }

        public void Log(LogEntry log)
        {
            _logger.Information($"[{log.Timestamp}] {log.Level} - {log.Source}: {log.Message}");
        }

        public async Task LogAsync(LogEntry log)
        {
            await Task.Run(() => Log(log));
        }

        public async Task LogAsync(LogLevel level, string message, string source)
        {
            var logEntry = new LogEntry
            {
                Level = level,
                Message = message,
                Source = source
            };
            await LogAsync(logEntry);
        }
    }
}
