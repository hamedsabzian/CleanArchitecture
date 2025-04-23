namespace Todo.Application.Commands.DoneToDo;

public record DoneToDoCommand(Guid Id) : IRequest<Response>;
