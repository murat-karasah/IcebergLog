using IcebergLog.API.gRPC;
using IcebergLog.Core.Config;
using IcebergLog.Core.Interfaces;
using IcebergLog.Core.Models;
using IcebergLog.Core.Services;
using IcebergLog.MongoDB;
using IcebergLog.PostgreSQL;
using IcebergLog.Elasticsearch;
using IcebergLog.API.Services;

namespace IcebergLog.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Logging add
            builder.Services.AddSingleton<ILoggerService, LoggerService>();

            // Controller  add
            builder.Services.AddControllers();

            // Swagger UI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // StorageConfig  
            var storageConfig = new StorageConfig
            {
                StorageType = builder.Configuration["LogStorage:Type"] ?? "MongoDB",
                ConnectionString = builder.Configuration["LogStorage:ConnectionString"] ?? "mongodb://localhost:27017"
            };

            //AddSingleton
            builder.Services.AddSingleton<ILoggerService>(_ =>
                LogServiceFactory.CreateLogger(storageConfig.StorageType, storageConfig.ConnectionString));

            // gRPC add
            builder.Services.AddGrpc();

            var app = builder.Build(); 
             
            // gRPC service add
            app.MapGrpcService<LogServiceGrpc>();

            // JSON API endpoint
            app.MapPost("/log", async (ILoggerService logger, LogEntry log) =>
            {
                await logger.LogAsync(log);
                return Results.Ok(new { message = "Log saved successfully" });
            });

            var minLogLevelStr = builder.Configuration["LogConfig:MinLogLevel"] ?? "Info";

            if (!Enum.TryParse<IcebergLog.Core.Models.LogLevel>(minLogLevelStr, true, out var minLogLevel))
            {
                minLogLevel = IcebergLog.Core.Models.LogLevel.Info; 
            }


            app.MapPost("/log/filter", async (ILoggerService logger, LogEntry log) =>
            {
                if (!LogFilter.ShouldLog((IcebergLog.Core.Models.LogLevel)log.Level, minLogLevel))
                {
                    return Results.BadRequest("Logs at this level are not saved.");
                }
                await logger.LogAsync(log);
                return Results.Ok(new { message = "Log saved\r\n" });
            });



            // Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
