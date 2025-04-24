using Todo.Application.Shared.Interfaces;
using Todo.Domain.Entities.Common;
using Todo.Domain.Events.Common;

namespace Todo.Application.Events.Common;

public class MediatorDomainEventDispatcher(IMediator mediator, ILogger<MediatorDomainEventDispatcher> logger) : IDomainEventDispatcher
{
    public async Task DispatchAndClearEvents(IEnumerable<IHasDomainEvents> entitiesWithEvents)
    {
        foreach (IHasDomainEvents entity in entitiesWithEvents)
        {
            if (entity is HasDomainEventsBase hasDomainEvents)
            {
                DomainEventBase[] events = hasDomainEvents.DomainEvents.ToArray();
                hasDomainEvents.ClearDomainEvents();

                foreach (DomainEventBase domainEvent in events)
                    await mediator.Publish(domainEvent).ConfigureAwait(false);
            }
            else
            {
                logger.LogError(
                    "Entity of type {EntityType} does not inherit from {BaseType}. Unable to clear domain events",
                    entity.GetType().Name,
                    nameof(HasDomainEventsBase));
            }
        }
    }
}
