using e_commercial.Exceptions;
using System.Text.Json;

namespace e_commercial.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _nextRequest;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        public GlobalExceptionMiddleware(RequestDelegate nextRequest, ILogger<GlobalExceptionMiddleware> logger)
        {
            _nextRequest = nextRequest;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _nextRequest(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json"; // Set the response content type to JSON

            context.Response.StatusCode = ex switch
            {
                ArgumentNullException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                BadValidationException=> StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            var result = JsonSerializer.Serialize(new
            {
                error = ex.Message,
                statusCode = context.Response.StatusCode
            });

            return context.Response.WriteAsync(result);
        }
    }
}
