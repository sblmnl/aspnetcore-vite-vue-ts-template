using Microsoft.AspNetCore.Diagnostics;

namespace MyApp.Web.Server;

internal sealed class ExceptionHandlerMiddleware : IExceptionHandler
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not OperationCanceledException)
        {
            _logger.LogError(
                exception,
                "Exception occurred: {Message} {StackTrace}",
                exception.Message,
                exception.StackTrace);
        }

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsync("An error occurred while processing your request!", cancellationToken);

        return true;
    }
}
