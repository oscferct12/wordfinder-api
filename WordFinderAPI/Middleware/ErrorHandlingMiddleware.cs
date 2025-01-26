
using System.Net;
using System.Text.Json;


namespace WordFinderAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = exception switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
                Details = exception.InnerException?.Message
            };

            var responseJson = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(responseJson);
        }

    }
}
