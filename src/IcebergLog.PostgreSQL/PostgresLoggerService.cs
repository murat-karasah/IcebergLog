using IcebergLog.Core.Interfaces;
using IcebergLog.Core.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace IcebergLog.PostgreSQL
{
    public class PostgresLoggerService : ILoggerService
    {
        private readonly string _connectionString;

        public PostgresLoggerService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Log(LogEntry logEntry)
        {
            Console.WriteLine($"[PostgreSQL] {logEntry.Timestamp}: {logEntry.Level} - {logEntry.Source}: {logEntry.Message}");
        }

        public async Task LogAsync(LogEntry logEntry)
        {
            var query = "INSERT INTO logs (timestamp, level, source, message) VALUES (@timestamp, @level, @source, @message)";

            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@timestamp", logEntry.Timestamp);
            command.Parameters.AddWithValue("@level", logEntry.Level.ToString());
            command.Parameters.AddWithValue("@source", logEntry.Source);
            command.Parameters.AddWithValue("@message", logEntry.Message);

            await command.ExecuteNonQueryAsync();
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
