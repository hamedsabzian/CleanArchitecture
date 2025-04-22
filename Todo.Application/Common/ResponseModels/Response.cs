namespace Todo.Application.Common.ResponseModels;

public class Response : ResponseBase<Response>;

public class Response<T> : ResponseBase<Response<T>>
{
    public T? Data { get; protected set; }

    public static Response<T> Success(T data)
    {
        return new Response<T> { Data = data }.Success();
    }
}
