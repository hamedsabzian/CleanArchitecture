using Todo.Domain.Entities.Common;

namespace Todo.Application.Abstraction.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<IHasDomainEvents> entitiesWithEvents);
}
