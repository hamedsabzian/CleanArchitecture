using Mediator;

namespace Todo.Domain.Events.Common;

public abstract record DomainEventBase(DateTime OccurredAt) : INotification;
