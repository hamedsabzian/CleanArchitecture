using Microsoft.Extensions.Caching.Memory;
using Todo.Application.Abstraction.Interfaces;
using Todo.Domain.Events;

namespace Todo.Application.Events;

public class ToDoUpdatedEventHandler(IMemoryCache cache, IToDoReader reader)
    : ToDoUpdatedEventBaseHandler(cache, reader), INotificationHandler<ToDoUpdatedEvent>
{
    public ValueTask Handle(ToDoUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return SyncCache(notification, cancellationToken);
    }
}