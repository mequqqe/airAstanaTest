using AirAstanaService.AirAstanaService.Infrastructure.Logs;
using Serilog;

namespace AirAstanaService.Application.Services;

public class LoggerService : ILoggerService
{
    public void LogInformation(string message, params object[] args)
    {
        Log.Information(message, args);
    }
}