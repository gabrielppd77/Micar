using Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.ExceptionHandling;

internal sealed class GlobalExceptionHandler() : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Title = "Ocorreu um erro inesperado, por favor entre em contato com o suporte pelo número (32)984816780",
            Detail = exception.Message,
        };

        if (exception is NotFoundException)
        {
            problemDetails.Status = StatusCodes.Status404NotFound;
            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4";
            problemDetails.Title = "O recurso solicitado não foi encontrado";
        }

        if (exception is BadRequestException)
        {
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
            problemDetails.Title = "Ocorreu um erro com a sua solicitação, verifique as informações";
        }

        if (exception is UnauthorizedException)
        {
            problemDetails.Status = StatusCodes.Status401Unauthorized;
            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1";
            problemDetails.Title = "Não autorizado";
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
