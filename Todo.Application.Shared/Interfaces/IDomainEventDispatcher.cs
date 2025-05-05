using Todo.Domain.Entities.Common;

namespace Todo.Application.Shared.Interfaces;

internal interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<IHasDomainEvents> entitiesWithEvents);
}
