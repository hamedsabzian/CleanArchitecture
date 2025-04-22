namespace Todo.Application.Commands.CreateToDo;

public class CreateToDoCommandHandler : IRequestHandler<CreateToDoCommand, Response<Guid>>
{
    public ValueTask<Response<Guid>> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
