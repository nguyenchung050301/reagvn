using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using System.Text.Json;

namespace e_commercial.Exceptions
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<JsonExceptionFilter> _logger;

        public JsonExceptionFilter(ILogger<JsonExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is JsonException jsonException)
            {
                _logger.LogError(jsonException, "JSON parse error");

                context.Result = new BadRequestObjectResult(new
                {
                    StatusCode = 400,
                    Message = "Invalid JSON format",
                    Details = jsonException.Message
                });
                context.ExceptionHandled = true;
            }
        }
    }
}
