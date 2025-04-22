namespace Todo.Application.Common.Behaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse>(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResponseBase<TResponse>, new()
{
    public async ValueTask<TResponse> Handle(
        TRequest message, CancellationToken cancellationToken, MessageHandlerDelegate<TRequest, TResponse> next)
    {
        try
        {
            return await next(message, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception for: {@Request}", message);

            ResponseBase<TResponse> result = new TResponse();
            result.WithStatus(DefaultResponseStatusCodes.Error).WithMessage("Unhandled exception");

            return (TResponse)result;
        }
    }
}
