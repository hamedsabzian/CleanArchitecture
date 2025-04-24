using Todo.Domain.Enums;

namespace Todo.Application.Shared.Dtos;

public record GetToDoDto(Guid Id, string Title, string? Description, ToDoStatus Status, DateTime CreatedAt);
