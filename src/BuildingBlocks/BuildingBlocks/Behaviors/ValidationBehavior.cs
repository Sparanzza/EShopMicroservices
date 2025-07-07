using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validator;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(_validator.Select(v => v.ValidateAsync(context, cancellationToken)));
        
        var failedResults = validationResults
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .ToList();
        if (failedResults.Any())
        {
            throw new ValidationException(failedResults);
        }
        // If validation passes, proceed to the next behavior or handler
        return await next(cancellationToken);
    }
}