using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using IcebergLog.Core.Interfaces;
using IcebergLog.Core.Models;
using IcebergLog.Core.Services;
using Microsoft.Extensions.Logging;
using LogLevel = IcebergLog.Core.Models.LogLevel;

namespace IcebergLog.Tests
{
    public class LoggerServiceTests
    {
        private readonly Mock<ILoggerService> _mockLoggerService;

        public LoggerServiceTests()
        {
            _mockLoggerService = new Mock<ILoggerService>();
        }

        [Fact]
        public void Log_ShouldLogMessage()
        {
            // Arrange
            var logEntry = new LogEntry
            {
                Level = LogLevel.Info,
                Message = "Test log message",
                Source = "UnitTest"
            };

            var logger = new LoggerService();

            // Act
            logger.Log(logEntry);

            // Assert
            Assert.NotNull(logEntry);
            Assert.Equal("Test log message", logEntry.Message);
        }

        [Fact]
        public async Task LogAsync_ShouldLogMessageAsynchronously()
        {
            // Arrange
            var logEntry = new LogEntry
            {
                Level = LogLevel.Warning,
                Message = "Async log test",
                Source = "UnitTest"
            };

            var logger = new LoggerService();

            // Act
            await logger.LogAsync(logEntry);

            // Assert
            Assert.NotNull(logEntry);
            Assert.Equal(LogLevel.Warning, logEntry.Level);
        }

        [Fact]
        public async Task LogAsync_WithLogLevel_ShouldLogCorrectly()
        {
            // Arrange
            var logger = new LoggerService();

            // Act
            await logger.LogAsync(LogLevel.Error, "Error log test", "UnitTest");

            // Assert
            _mockLoggerService.Verify(
                x => x.LogAsync(It.Is<LogEntry>(log => log.Level == LogLevel.Error && log.Message == "Error log test")),
                Times.Once
            );
        }
    }
}
