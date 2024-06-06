using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace NET8_GlobalExceptionHandling
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("An error occurred: {message}", exception.Message);

            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred",
                Detail = exception.Message,
                Status = (int)HttpStatusCode.InternalServerError,
                Type = exception.GetType().Name
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
