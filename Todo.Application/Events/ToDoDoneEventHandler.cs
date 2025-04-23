using Microsoft.Extensions.Caching.Memory;
using Todo.Application.Abstraction.Interfaces;
using Todo.Domain.Events;

namespace Todo.Application.Events;

public class ToDoDoneEventHandler(IMemoryCache cache, IToDoReader reader)
    : ToDoUpdatedEventBaseHandler(cache, reader), INotificationHandler<ToDoDoneEvent>
{
    public ValueTask Handle(ToDoDoneEvent notification, CancellationToken cancellationToken)
    {
        return SyncCache(notification, cancellationToken);
    }
}