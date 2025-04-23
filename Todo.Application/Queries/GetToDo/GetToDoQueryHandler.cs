using Todo.Application.Abstraction.Dtos;
using Todo.Application.Abstraction.Interfaces;

namespace Todo.Application.Queries.GetToDo;

public class GetToDoQueryHandler(IToDoReader reader, ILogger<GetToDoQueryHandler> logger)
    : IRequestHandler<GetToDoQuery, Response<GetToDoDto>>
{
    public async ValueTask<Response<GetToDoDto>> Handle(GetToDoQuery request, CancellationToken cancellationToken)
    {
        var todo = await reader.GetAsync(request.Id, cancellationToken);
        if (todo is null)
        {
            logger.LogWarning("The todo with id {Id} was not found", request.Id);
            return new Response<GetToDoDto>().NotFound();
        }

        return new Response<GetToDoDto>().Success(todo);
    }
}
