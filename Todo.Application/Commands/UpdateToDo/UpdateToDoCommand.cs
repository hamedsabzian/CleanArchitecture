namespace Todo.Application.Commands.UpdateToDo;

public record UpdateToDoCommand(Guid Id,string Title, string? Description) : IRequest<Response>;
