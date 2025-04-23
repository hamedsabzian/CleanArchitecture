namespace Todo.Application.Commands.ActivateToDo;

public record ActivateToDoCommand(Guid Id) : IRequest<Response>;
