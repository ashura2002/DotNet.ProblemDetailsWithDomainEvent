using Api.Common;
using Application.Common.Errors;
using Application.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions
{
    public static class ActionResultExtensions
    {
        public static ActionResult<T> ToActionResult<T>(this ControllerBase controller, ResultWithReturnValue<T> resultWithReturnValue)
        {
            if (resultWithReturnValue.IsSuccess)
                return controller.Ok(resultWithReturnValue.Value);

            return controller.ToErrorActionResult(resultWithReturnValue.Error!);
        }

        public static ActionResult ToActionResult(this ControllerBase controller, Result result)
        {
            if (result.IsSuccess)
                return controller.NoContent();

            return controller.ToErrorActionResult(result.Error!);
        }

        private static ObjectResult ToErrorActionResult(this ControllerBase controller, Error error)
        {

            var (statusCode, title) = ErrorResponseMapper.Map(error);

            var problemDetails = ProblemDetailsFactory.Create(
                controller.HttpContext,
                statusCode,
                title,
                statusCode >= 500
                ? "An Unexpected error occured."
                : error.ErrorMessage, // para sa details
                statusCode >= 500  
                ? ApiErrorCodes.InternalServerError
                : error.ErrorCode);

            return new ObjectResult(problemDetails)
            {
                StatusCode = statusCode,
                ContentTypes = { "application/problem+json" }
            };
        }
    }
}