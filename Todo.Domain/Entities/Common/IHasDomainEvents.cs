using Todo.Domain.Events.Common;

namespace Todo.Domain.Entities.Common;

public interface IHasDomainEvents
{
    IReadOnlyCollection<DomainEventBase> DomainEvents { get; }
}
