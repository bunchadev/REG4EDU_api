using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace REG4EDU_api.Middlewares
{
    public class ExceptionHandlerMiddlewares(ILogger<ExceptionHandlerMiddleware> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception.ToString());
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server error",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
            };
            httpContext.Response.StatusCode = problemDetails.Status ?? 0;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
            return true;
        }
    }
}

