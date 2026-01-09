using System.Net;
using System.Text.Json;
using TravelBooking.Api.Models; // ApiResponse'u kullanmak için

namespace TravelBooking.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message = "Bir hata oluştu.";

            // Özel hataları yakalama (UserService'ten fırlatılan özel mesajları kullanır)
            if (exception is UnauthorizedAccessException)
            {
                status = HttpStatusCode.Unauthorized; // 401
                message = exception.Message;
            }
            else if (exception.Message.Contains("Conflict"))
            {
                status = HttpStatusCode.Conflict; // 409
                message = exception.Message;
            }
            else if (exception.Message.Contains("Not Found"))
            {
                status = HttpStatusCode.NotFound; // 404
                message = exception.Message;
            }
            
            _logger.LogError(exception, "Global Exception Caught: {Message}", exception.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var response = new ApiResponse<object>(null, message, false); // success: false

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}