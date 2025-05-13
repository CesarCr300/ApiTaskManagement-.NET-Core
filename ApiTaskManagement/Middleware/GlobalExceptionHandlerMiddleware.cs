namespace ApiTaskManagement.Middleware
{
    using System.Net;
    using System.Text.Json;
    using ApiTaskManagement.Utils;

    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = ResponseHandler.Error("An unexpected error occurred", 500);
                var result = JsonSerializer.Serialize(errorResponse);

                await context.Response.WriteAsync(result);
            }
        }
    }
}
