using Todo.Domain.Entities.Common;

namespace Todo.Application.Shared.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<IHasDomainEvents> entitiesWithEvents);
}
