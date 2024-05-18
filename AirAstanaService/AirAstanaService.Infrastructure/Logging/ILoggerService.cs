namespace AirAstanaService.AirAstanaService.Infrastructure.Logs;

public interface ILoggerService
{
    void LogInformation(string message, params object[] args);
}