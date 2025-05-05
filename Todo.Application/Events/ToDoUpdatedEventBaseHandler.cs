using Microsoft.Extensions.Caching.Memory;
using Todo.Application.Shared.Dtos;
using Todo.Application.Shared.Interfaces;
using Todo.Domain.Enums;
using Todo.Domain.Events;

namespace Todo.Application.Events;

internal abstract class ToDoUpdatedEventBaseHandler(IMemoryCache cache, IToDoReader reader)
{
    private const int CacheDurationInMinutes = 10;

    protected async ValueTask SyncCache(ToDoUpdatedEventBase notification, CancellationToken cancellationToken)
    {
        var cacheKey = $"todo_{notification.Id}";
        var todo = cache.Get<GetToDoDto>(notification.Id);
        if (todo is not null)
        {
            cache.Set(cacheKey, todo with { Status = ToDoStatus.Activated }, TimeSpan.FromMinutes(CacheDurationInMinutes));
            return;
        }


        todo = await reader.GetAsync(notification.Id, cancellationToken);
        if (todo is null)
        {
            return;
        }

        cache.Set(cacheKey, todo, TimeSpan.FromMinutes(CacheDurationInMinutes));
    }
}
