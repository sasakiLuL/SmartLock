using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SmartLock.Domain.Core.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace SmartLock.Api.Middlewares;

internal class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        var problemDetails = exception switch
        {
            NotFoundException notFound => new ProblemDetails
            {
                Title = "NotFound",
                Detail = notFound.Message,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                Status = StatusCodes.Status404NotFound,
            },
            BadRequestException badRequest => new ProblemDetails
            {
                Title = "BadRequest",
                Detail = badRequest.Message,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Status = StatusCodes.Status400BadRequest,
                Extensions = new Dictionary<string, object?>
                {
                    { "errors", badRequest.Error }
                }
            },
            ForbiddenException forbidden => new ProblemDetails
            {
                Title = "Forbidden",
                Detail = forbidden.Message,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
                Status = StatusCodes.Status403Forbidden,
            },
            ValidationException validation => new ProblemDetails
            {
                Title = "BadRequest",
                Detail = BadRequestException.ValidationErrorMessage,
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Status = StatusCodes.Status400BadRequest,
                Extensions = new Dictionary<string, object?>
                {
                    { "errors", validation.Errors }
                }
            },
            _ => new ProblemDetails
            {
                Title = "InternalServerError",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                Status = StatusCodes.Status500InternalServerError,
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status!.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
