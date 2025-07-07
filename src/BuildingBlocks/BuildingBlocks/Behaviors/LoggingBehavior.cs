using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] handle request = {@Request}, response={@Response}, details = {@Details}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();
        var response = await next(cancellationToken);
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning(
                "[PERFORMANCE] handle request = {@Request}, response={@Response}, details = {@Details}, timeTaken={@TimeTaken}",
                typeof(TRequest).Name, typeof(TResponse).Name, request, timeTaken);
        }

        logger.LogInformation(
            "[END] handle request = {@Request}, response={@Response}, details = {@Details}, timeTaken={@TimeTaken}",
            typeof(TRequest).Name, typeof(TResponse).Name, request, timeTaken);

        return response;
    }
}