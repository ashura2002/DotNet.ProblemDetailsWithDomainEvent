using Api.Common;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares
{
    public sealed class GlobalException : IExceptionHandler
    {
        private readonly ILogger<GlobalException> _logger;
        public GlobalException(ILogger<GlobalException> logger)
        {
            _logger = logger;
        }


        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Exception {Exception}", exception.Message);

            var (statusCode, title, detail, errorCode) = exception switch
            {
                ValidationException => (
                StatusCodes.Status400BadRequest,
                "Validation Failed",
                "One or more validation occured.",
                ApiErrorCodes.ValidationError),

                DomainRuleException => (
                StatusCodes.Status400BadRequest,
                "Bad Request",
                exception.Message,
                ApiErrorCodes.DomainRuleException),

                _ => (StatusCodes.Status500InternalServerError,
                "Internal Server Error", "An unexpected error occurred.",
                ApiErrorCodes.InternalServerError)
            };


            if (statusCode >= 500)
                _logger.LogError(exception, "Unhandled exception occurred.");
            else
                _logger.LogWarning(exception, "Request failed.");


            ProblemDetails problemDetails;

            if (exception is ValidationException validationException)
            {
                var errors = validationException.Errors
                    .GroupBy(commandProperty => commandProperty.PropertyName)
                    .ToDictionary(
                    group => group.Key,
                    group => group.Select(error => error.ErrorMessage)
                    .ToArray());

                problemDetails = new ValidationProblemDetails(errors)
                {
                    Status = statusCode,
                    Title = title,
                    Detail = detail,
                    Instance = httpContext.Request.Path
                };


                // extension
                problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
                problemDetails.Extensions["errorCode"] = errorCode;
            }
            else
            {
                problemDetails = ProblemDetailsFactory.Create(
                    httpContext,
                    statusCode,
                    title, 
                    detail, 
                    errorCode);
            }


            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
