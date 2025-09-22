using CrmApp.Domain.Exceptions;
using CrmApp.Shared.DTO;
using System.Net;

namespace CrmApp.Server.Middleware
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
                await _next(context); // continue pipeline
            }
            catch (DuplicateEmailException ex) // Domain business rule
            {
                _logger.LogWarning(ex, "Duplicate email detected");

                var response = new ErrorResponse
                {
                    Type = nameof(DuplicateEmailException),
                    Errors = new() { ex.Message }
                };

                await WriteResponseAsync(context, HttpStatusCode.Conflict, response);
            }
            catch (DomainException ex) // Other domain rules
            {
                _logger.LogWarning(ex, "Domain error");

                var response = new ErrorResponse
                {
                    Type = ex.GetType().Name,
                    Errors = new() { ex.Message }
                };

                await WriteResponseAsync(context, HttpStatusCode.BadRequest, response);
            }
            catch (Exception ex) // Unhandled
            {
                _logger.LogError(ex, "Unhandled server error");

                var response = new ErrorResponse
                {
                    Type = "ServerError",
                    Errors = new() { "An unexpected error occurred. Please try again later." }
                };

                await WriteResponseAsync(context, HttpStatusCode.InternalServerError, response);
            }
        }

        private static async Task WriteResponseAsync(HttpContext context, HttpStatusCode code, ErrorResponse response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}