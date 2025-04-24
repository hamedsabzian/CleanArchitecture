using Todo.Domain.Enums;

namespace Todo.Application.Shared.Dtos;

public record GetToDoListDto(Guid Id, string Title, ToDoStatus Status, DateTime CreatedAt);
