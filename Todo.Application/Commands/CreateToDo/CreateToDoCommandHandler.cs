using Todo.Application.Shared.Interfaces;
using Todo.Application.Common;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.CreateToDo;

public class CreateToDoCommandHandler(
    IIdGenerator idGenerator,
    TimeProvider timeProvider,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateToDoCommand, Response<Guid?>>
{
    public async ValueTask<Response<Guid?>> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
    {
        var todo = new ToDo(idGenerator.New<ToDo>(), request.Title, request.Description, timeProvider.GetUtcNow().DateTime);

        unitOfWork.Repository<ToDo>().Add(todo);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new Response<Guid?>().Success(todo.Id);
    }
}
