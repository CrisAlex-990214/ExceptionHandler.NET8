﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace NET8_GlobalExceptionHandling
{
    public class BadRequestExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is BadHttpRequestException)
            {
                logger.LogError("Bad Request: {message}", exception.Message);

                var problemDetails = new ProblemDetails
                {
                    Title = "Bad Request",
                    Detail = exception.Message,
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = exception.GetType().Name
                };

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

                return true;
            }

            return false;
        }
    }
}
