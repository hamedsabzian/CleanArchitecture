namespace Todo.Application.Common.ResponseModels;

public class Response : ResponseBase<Response>;

public class Response<T> : ResponseBase<Response<T>>
{
    public T? Data { get; protected set; }

    public Response<T> Success(T data)
    {
        Success();
        Data = data;
        return this;
    }
}
