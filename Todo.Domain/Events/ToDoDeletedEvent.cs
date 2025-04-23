using Todo.Domain.Events.Common;

namespace Todo.Domain.Events;

public sealed record ToDoDeletedEvent(Guid Id, DateTime OccurredAt) : DomainEventBase(OccurredAt);
