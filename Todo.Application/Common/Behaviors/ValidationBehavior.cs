using FluentValidation;

namespace Todo.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResponseBase<TResponse>, new()
{
    public async ValueTask<TResponse> Handle(
        TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {
        if (validators.Any())
        {
            var context = new ValidationContext<TRequest>(message);

            var validationResults = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Count > 0)
            {
                logger.LogWarning("Request is invalid: {@Request}", message);

                ResponseBase<TResponse> result = new TResponse()
                    .Invalid()
                    .WithErrors(failures.Select(a => new ResponseError(a.PropertyName, a.ErrorMessage)));

                return (TResponse)result;
            }
        }

        return await next(message, cancellationToken);
    }
}
