using Todo.Application.Abstraction.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.DeleteToDo;

public class DeleteToDoCommandHandler(
    IUnitOfWork unitOfWork,
    ILogger<DeleteToDoCommandHandler> logger) : IRequestHandler<DeleteToDoCommand, Response>
{
    public async ValueTask<Response> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ToDo>();
        var todo = await repository.GetAsync(request.Id, cancellationToken);
        if (todo is null)
        {
            logger.LogWarning("The todo with id {Id} was not found", request.Id);
            return new Response().NotFound();
        }

        repository.Delete(todo);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("The todo with id {Id} was successfully deleted", request.Id);

        return new Response().Success();
    }
}
