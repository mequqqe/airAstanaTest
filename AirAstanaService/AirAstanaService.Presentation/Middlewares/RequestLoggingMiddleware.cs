using AirAstanaService.AirAstanaService.Infrastructure.Logs;
namespace AirAstanaService.Presentation.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger; // Использование интерфейса логгера

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userName = context.User?.Identity?.Name ?? "Anonymous";
            var requestTime = DateTime.Now;

            _logger.LogInformation("User: {User}, Time: {Time}, Method: {Method}, Path: {Path}",
                userName, requestTime, context.Request.Method, context.Request.Path);

            await _next(context);
        }
    }
}