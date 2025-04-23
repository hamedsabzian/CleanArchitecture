namespace Todo.Domain.Events;

public sealed record ToDoActivatedEvent(Guid Id, DateTime OccurredAt) : ToDoUpdatedEventBase(Id, OccurredAt);
