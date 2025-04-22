namespace Todo.Application.Commands.CreateToDo;

public record CreateToDoCommand(string Title, string? Description) : IRequest<Response<Guid>>;
