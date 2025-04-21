using Ardalis.GuardClauses;
using Todo.Domain.Enums;
using Todo.Domain.Extensions;

namespace Todo.Domain.Entities;

public class ToDo
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

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public ToDoStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public byte[] RowVersion { get; private set; }

    public void Todo(DateTime now)
    {
        Status = ToDoStatus.ToDo;
        UpdatedAt = now;
    }

    public void Done(DateTime now)
    {
        Guard.Against.NotEqualTo(Status, ToDoStatus.ToDo, nameof(Status));

        Status = ToDoStatus.Done;
        UpdatedAt = now;
    }

    public void Update(string title, string description, DateTime now)
    {
        Guard.Against.NullOrEmpty(title);

        Title = title;
        Description = description;
        UpdatedAt = now;
    }
}