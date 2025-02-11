using Grpc.Core;
using IcebergLog.Core.Interfaces;
using IcebergLog.Core.Models;
using Microsoft.AspNetCore.Identity.Data;
using LogLevel = IcebergLog.Core.Models.LogLevel;

namespace IcebergLog.API.gRPC
{
    public class LogServiceGrpc : LogService.LogServiceBase
    {
        private readonly ILoggerService _logger;

        public LogServiceGrpc(ILoggerService logger)
        {
            _logger = logger;
        }

        public override async Task<LogResponse> WriteLog(LogRequest request, ServerCallContext context)
        {
            var level = LogLevel.Info; 

            if (Enum.TryParse<LogLevel>(request.Level, true, out var parsedLevel))
            {
                level = parsedLevel;
            }

          
            var logEntry = new LogEntry(level, request.Message, request.Source);

            await _logger.LogAsync(logEntry);

            return new LogResponse { Success = true, Message = "Log saved successfully" };
        }
    }
}
