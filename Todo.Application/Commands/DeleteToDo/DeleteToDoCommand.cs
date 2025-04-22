namespace Todo.Application.Commands.DeleteToDo;

public record DeleteToDoCommand(Guid Id) : IRequest<Response>;
