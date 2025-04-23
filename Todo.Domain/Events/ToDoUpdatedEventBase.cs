using Todo.Domain.Events.Common;

namespace Todo.Domain.Events;

public abstract record ToDoUpdatedEventBase(Guid Id, DateTime OccurredAt) : DomainEventBase(OccurredAt);
