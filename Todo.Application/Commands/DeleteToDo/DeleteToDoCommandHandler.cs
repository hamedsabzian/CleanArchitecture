using Todo.Application.Shared.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.DeleteToDo;

internal class DeleteToDoCommandHandler(
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider,
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

        todo.Delete(timeProvider.GetUtcNow().DateTime);
        repository.Delete(todo);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("The todo with id {Id} was successfully deleted", request.Id);

        return new Response().Success();
    }
}
