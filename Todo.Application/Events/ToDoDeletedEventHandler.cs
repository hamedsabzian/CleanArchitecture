using Microsoft.Extensions.Caching.Memory;
using Todo.Domain.Events;

namespace Todo.Application.Events;

public class ToDoDeletedEventHandler(IMemoryCache cache) : INotificationHandler<ToDoDeletedEvent>
{
    public ValueTask Handle(ToDoDeletedEvent notification, CancellationToken cancellationToken)
    {
        cache.Remove($"todo_{notification.Id}");
        return default;
    }
}
