using Microsoft.AspNetCore.Mvc;

namespace Api.Common
{
    public static class ProblemDetailsFactory
    {
        public static ProblemDetails Create(
            HttpContext httpContext,
            int statusCode,
            string title,
            string detail,
            string? errorCode = null,
            IDictionary<string, string[]>? errors = null)
        {
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path
            };

            // property to extend
            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;


            // para sa validation context sa fluent validation
            if (errorCode is not null)
                problemDetails.Extensions["errorCode"] = errorCode;

            // actual error message from fluent validation
            if (errors is not null)
                problemDetails.Extensions["errors"] = errors;


            return problemDetails;
        }

    }
}
