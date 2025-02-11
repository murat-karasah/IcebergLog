using IcebergLog.Core.Interfaces;
using IcebergLog.Core.Models;
using System;
using System.Threading.Tasks;

namespace IcebergLog.MongoDB
{
    public class MongoLoggerService : ILoggerService
    {
        private readonly string _connectionString;

        public MongoLoggerService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Log(LogEntry logEntry)
        {
            Console.WriteLine($"[MongoDB] {logEntry.Timestamp}: {logEntry.Level} - {logEntry.Source}: {logEntry.Message}");
        }

        public async Task LogAsync(LogEntry logEntry)
        {
            await Task.Run(() => Log(logEntry));
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
