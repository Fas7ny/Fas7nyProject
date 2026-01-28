using System.Net;
using System.Text.Json;

namespace Fas7nyProject.Presentation.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log the exception
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            // Prepare the response
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse();

            switch (exception)
            {
                case ApplicationException appEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = appEx.Message,
                        Details = _environment.IsDevelopment() ? appEx.StackTrace : null
                    };
                    break;

                case KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Message = "The requested resource was not found.",
                        Details = _environment.IsDevelopment() ? exception.StackTrace : null
                    };
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)HttpStatusCode.Unauthorized,
                        Message = "Unauthorized access.",
                        Details = _environment.IsDevelopment() ? exception.StackTrace : null
                    };
                    break;

                case ArgumentNullException argNullEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = $"Invalid input: {argNullEx.ParamName}",
                        Details = _environment.IsDevelopment() ? argNullEx.StackTrace : null
                    };
                    break;

                case ArgumentException argEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = argEx.Message,
                        Details = _environment.IsDevelopment() ? argEx.StackTrace : null
                    };
                    break;

                case InvalidOperationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = exception.Message,
                        Details = _environment.IsDevelopment() ? exception.StackTrace : null
                    };
                    break;

                case HttpRequestException httpEx:
                    context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)HttpStatusCode.ServiceUnavailable,
                        Message = "External service unavailable. Please try again later.",
                        Details = _environment.IsDevelopment() ? httpEx.StackTrace : null
                    };
                    break;

                case JsonException jsonEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Message = "Invalid JSON format in request or response.",
                        Details = _environment.IsDevelopment() ? jsonEx.StackTrace : null
                    };
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = _environment.IsDevelopment()
                            ? exception.Message
                            : "An internal server error occurred. Please try again later.",
                        Details = _environment.IsDevelopment() ? exception.StackTrace : null
                    };
                    break;
            }

            // Add request information for debugging
            if (_environment.IsDevelopment())
            {
                response.Path = context.Request.Path;
                response.Method = context.Request.Method;
                response.Timestamp = DateTime.UtcNow;
            }

            // Serialize and write response
            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = _environment.IsDevelopment()
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }

    // Error Response Model
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; }
        public string? Path { get; set; }
        public string? Method { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}