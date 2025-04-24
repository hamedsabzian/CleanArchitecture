using Microsoft.Extensions.Caching.Memory;
using Todo.Application.Shared.Dtos;
using Todo.Application.Shared.Interfaces;
using Todo.Application.Common;

namespace Todo.Application.Queries.GetToDo;

public class GetToDoQueryHandler(IToDoReader reader, IMemoryCache cache, ILogger<GetToDoQueryHandler> logger)
    : IRequestHandler<GetToDoQuery, Response<GetToDoDto>>
{
    public async ValueTask<Response<GetToDoDto>> Handle(GetToDoQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"todo_{request.Id}";

        var todo = await cache.GetWithFallbackAsync(cacheKey, clnToken => reader.GetAsync(request.Id, clnToken), cancellationToken);
        if (todo is null)
        {
            logger.LogWarning("The todo with id {Id} was not found", request.Id);
            return new Response<GetToDoDto>().NotFound();
        }

        return new Response<GetToDoDto>().Success(todo);
    }
}
