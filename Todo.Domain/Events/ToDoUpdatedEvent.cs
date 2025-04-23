namespace Todo.Domain.Events;

public sealed record ToDoUpdatedEvent(Guid Id, DateTime OccurredAt) : ToDoUpdatedEventBase(Id, OccurredAt);
