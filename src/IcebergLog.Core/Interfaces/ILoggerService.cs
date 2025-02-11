using IcebergLog.Core.Models;
using System;
using System.Threading.Tasks;

namespace IcebergLog.Core.Interfaces
{
    public interface ILoggerService
    {
        /// <summary>
        /// Adds a log entry synchronously (not recommended, async is preferred)
        /// </summary>
        void Log(LogEntry logEntry);

        /// <summary>
        /// Adds a log entry asynchronously
        /// </summary>
        Task LogAsync(LogEntry logEntry);

        /// <summary>
        /// Adds a log entry with a specific log level
        /// </summary>
        Task LogAsync(LogLevel level, string message, string source);
    }
}
