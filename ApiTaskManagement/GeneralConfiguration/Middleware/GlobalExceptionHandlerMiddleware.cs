namespace ApiTaskManagement.GeneralConfiguration.Middleware
{
    using System.ComponentModel;
    using System.Net;
    using System.Text.Json;
    using ApiTaskManagement.Utils;
    using ApiTaskManagement.Utils.Exceptions;

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

                context.Response.ContentType = "application/json";

                int httpStatusCode;
                string message;

                if (ex is HttpException httpException)
                {
                    context.Response.StatusCode = httpException.StatusCode;
                    httpStatusCode = httpException.StatusCode;
                    message = httpException.Message;
                }
                else
                {
                    _logger.LogError(ex, "Unhandled exception");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred";
                }

                var errorResponse = ResponseHandler.Error(message, httpStatusCode);
                var result = JsonSerializer.Serialize(errorResponse);

                await context.Response.WriteAsync(result);
            }
        }
    }
}
