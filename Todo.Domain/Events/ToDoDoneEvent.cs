namespace Todo.Domain.Events;

public sealed record ToDoDoneEvent(Guid Id, DateTime OccurredAt) : ToDoUpdatedEventBase(Id, OccurredAt);
