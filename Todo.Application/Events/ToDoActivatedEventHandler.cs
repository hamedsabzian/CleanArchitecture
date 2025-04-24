using Microsoft.Extensions.Caching.Memory;
using Todo.Application.Shared.Interfaces;
using Todo.Domain.Events;

namespace Todo.Application.Events;

public class ToDoActivatedEventHandler(IMemoryCache cache, IToDoReader reader)
    : ToDoUpdatedEventBaseHandler(cache, reader), INotificationHandler<ToDoActivatedEvent>
{
    public ValueTask Handle(ToDoActivatedEvent notification, CancellationToken cancellationToken)
    {
        return SyncCache(notification, cancellationToken);
    }
}
