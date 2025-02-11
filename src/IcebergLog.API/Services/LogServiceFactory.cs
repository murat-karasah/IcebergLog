using IcebergLog.Core.Interfaces;
using IcebergLog.Elasticsearch;
using IcebergLog.MongoDB;
using IcebergLog.PostgreSQL;
using System;

namespace IcebergLog.API.Services
{
    public static class LogServiceFactory  
    {
        public static ILoggerService CreateLogger(string storageType, string connectionString)  
        {
            return storageType switch
            {
                "MongoDB" => new MongoLoggerService(connectionString),
                "PostgreSQL" => new PostgresLoggerService(connectionString),
                "Elasticsearch" => new ElasticLoggerService(connectionString),
                _ => throw new ArgumentException("Geçersiz StorageType seçildi.")
            };
        }
    }
}
