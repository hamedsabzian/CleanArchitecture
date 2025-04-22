namespace Todo.Application.Common.ResponseModels;

public abstract class ResponseBase
{
    public int StatusCode { get; protected set; }

    public string? Message { get; protected set; }

    public IEnumerable<ResponseError>? Errors { get; protected set; }
}

public abstract class ResponseBase<TResponse> : ResponseBase
    where TResponse : ResponseBase<TResponse>
{
    public TResponse Success()
    {
        StatusCode = DefaultResponseStatusCodes.Ok;
        Message = "OK";
        return (TResponse)this;
    }

    public TResponse Error()
    {
        StatusCode = DefaultResponseStatusCodes.Error;
        Message = "Something went wrong";
        return (TResponse)this;
    }

    public TResponse NotFound()
    {
        StatusCode = DefaultResponseStatusCodes.NotFound;
        Message = "Not found";
        return (TResponse)this;
    }

    public TResponse Invalid()
    {
        StatusCode = DefaultResponseStatusCodes.Invalid;
        Message = "Request cannot be processed";
        return (TResponse)this;
    }

    public TResponse WithStatus(int statusCode)
    {
        StatusCode = statusCode;
        return (TResponse)this;
    }

    public TResponse WithMessage(string message)
    {
        Message = message;
        return (TResponse)this;
    }

    public TResponse WithErrors(IEnumerable<ResponseError> errors)
    {
        Errors = errors;
        return (TResponse)this;
    }
}
