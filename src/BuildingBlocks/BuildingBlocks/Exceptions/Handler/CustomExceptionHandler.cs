using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError("Error Message: {Message}, StackTrace: {StackTrace}, Time: {time}", exception.Message,
            exception.StackTrace, DateTime.UtcNow);

        (string HttpValidationProblemDetails, string Title, int StatusCode) = exception switch
        {
            InternalServerException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status500InternalServerError
            ),
            BadRequestException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status400BadRequest
            ),
            NotFoundException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status404NotFound
            ),
            ValidationException => (
                exception.Message,
                exception.GetType().Name,
                StatusCodes.Status400BadRequest
            ),
            _ => ("An unexpected error occurred.", "Internal Server Error", StatusCodes.Status500InternalServerError)
        };

        var problemDetails = new ProblemDetails
        {
            Title = Title,
            Status = StatusCode,
            Detail = HttpValidationProblemDetails,
            Instance = httpContext.Request.Path,
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationException", validationException.Errors);
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}