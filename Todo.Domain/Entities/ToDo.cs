using Ardalis.GuardClauses;
using Todo.Domain.Entities.Common;
using Todo.Domain.Enums;
using Todo.Domain.Events;
using Todo.Domain.Extensions;

namespace Todo.Domain.Entities;

public class ToDo : EntityBase<Guid>, IAggregateRoot
{
    private ToDo()
    {
    }

    public ToDo(Guid id, string title, string? description, DateTime now)
    {
        Guard.Against.NullOrEmpty(id);
        Guard.Against.NullOrEmpty(title);

        Id = id;
        Title = title;
        Description = description;
        CreatedAt = now;
        Status = ToDoStatus.Created;
    }

    public string Title { get; private set; }
    public string? Description { get; private set; }
    public ToDoStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public void Activate(DateTime now)
    {
        Status = ToDoStatus.Activated;
        UpdatedAt = now;

        RegisterDomainEvent(new ToDoActivatedEvent(Id, now));
    }

    public bool CanBeDone()
    {
        return Status == ToDoStatus.Activated;
    }

    public void Done(DateTime now)
    {
        Guard.Against.NotEqualTo(Status, ToDoStatus.Activated, nameof(Status));

        Status = ToDoStatus.Done;
        UpdatedAt = now;

        RegisterDomainEvent(new ToDoDoneEvent(Id, now));
    }

    public void Update(string title, string? description, DateTime now)
    {
        Guard.Against.NullOrEmpty(title);

        Title = title;
        Description = description;
        UpdatedAt = now;

        RegisterDomainEvent(new ToDoUpdatedEvent(Id, now));
    }

    public void Delete(DateTime now)
    {
        RegisterDomainEvent(new ToDoDeletedEvent(Id, now));
    }
}
