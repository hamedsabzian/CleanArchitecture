using Todo.Domain.Enums;

namespace Todo.Application.Abstraction.Dtos;

public record GetToDoDto(Guid Id, string Title, string? Description, ToDoStatus Status, DateTime CreatedAt);
