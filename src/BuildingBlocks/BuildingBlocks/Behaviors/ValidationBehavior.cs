using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest: ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var ctx = new ValidationContext<TRequest>(request);

        var validationResult = await Task.WhenAll(validators.Select(v => v.ValidateAsync(ctx, cancellationToken)));

        var failures = validationResult.Where(e => e.Errors.Any()).SelectMany(r => r.Errors).ToList();

        if (failures.Any())
            throw new ValidationException(failures);

        return await next();
    }
}