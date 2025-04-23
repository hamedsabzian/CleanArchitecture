using Todo.Domain.Enums;

namespace Todo.Application.Abstraction.Dtos;

public record GetToDoListDto(Guid Id, string Title, ToDoStatus Status, DateTime CreatedAt);
