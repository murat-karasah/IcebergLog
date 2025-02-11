using IcebergLog.Core.Interfaces;
using IcebergLog.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IcebergLog.Core.Services
{
    public class MultiLoggerService : ILoggerService
    {
        private readonly IEnumerable<ILoggerService> _loggers;

        public MultiLoggerService(IEnumerable<ILoggerService> loggers)
        {
            _loggers = loggers;
        }

        public void Log(LogEntry log)
        {
            foreach (var logger in _loggers)
            {
                logger.Log(log);
            }
        }

        public async Task LogAsync(LogEntry log)
        {
            var tasks = _loggers.Select(logger => logger.LogAsync(log));
            await Task.WhenAll(tasks);
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
