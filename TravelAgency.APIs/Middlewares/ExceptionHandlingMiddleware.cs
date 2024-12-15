using System.Net;
using System.Text.Json;

namespace TravelAgency.APIs.Middlewares
{

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An error occurred.");

            var response = context.Response;
            response.ContentType = "application/json";

            // Default response for unexpected errors
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var errorResponse = new
            {
                StatusCode = statusCode,
                Message = "An unexpected error occurred."
            };

            // Handle specific application exceptions
            if (exception is ApplicationException appEx)
            {
                statusCode = (int)HttpStatusCode.BadRequest; // Example for client errors
                errorResponse = new
                {
                    StatusCode = statusCode,
                    Message = appEx.Message // Send user-friendly message
                };
            }

            response.StatusCode = statusCode;

            // Serialize and write the response
            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(jsonResponse);
        }
    }


}
