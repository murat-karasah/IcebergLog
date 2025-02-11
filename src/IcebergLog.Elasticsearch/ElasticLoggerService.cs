using IcebergLog.Core.Interfaces;
using IcebergLog.Core.Models;
using System;
using System.Threading.Tasks;
using Nest;

namespace IcebergLog.Elasticsearch
{
    public class ElasticLoggerService : ILoggerService
    {
        private readonly ElasticClient _client;
        private readonly string _indexName = "logs";

        public ElasticLoggerService(string connectionString)
        {
            var settings = new ConnectionSettings(new Uri(connectionString))
                .DefaultIndex(_indexName);
            _client = new ElasticClient(settings);
        }

        public void Log(LogEntry logEntry)
        {
            Console.WriteLine($"[Elasticsearch] {logEntry.Timestamp}: {logEntry.Level} - {logEntry.Source}: {logEntry.Message}");
        }

        public async Task LogAsync(LogEntry logEntry)
        {
            var response = await _client.IndexDocumentAsync(logEntry);
            if (!response.IsValid)
            {
                Console.WriteLine($"[Elasticsearch] Log saving error: {response.DebugInformation}");
            }
        }

     
        public async Task LogAsync(IcebergLog.Core.Models.LogLevel level, string message, string source) 
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
