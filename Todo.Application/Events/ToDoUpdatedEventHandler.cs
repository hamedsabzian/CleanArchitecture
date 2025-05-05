using Microsoft.Extensions.Caching.Memory;
using Todo.Application.Shared.Interfaces;
using Todo.Domain.Events;

namespace Todo.Application.Events;

internal class ToDoUpdatedEventHandler(IMemoryCache cache, IToDoReader reader)
    : ToDoUpdatedEventBaseHandler(cache, reader), INotificationHandler<ToDoUpdatedEvent>
{
    public ValueTask Handle(ToDoUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return SyncCache(notification, cancellationToken);
    }
}
