using Todo.Application.Shared.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.ActivateToDo;

public class ActivateToDoCommandHandler(
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider,
    ILogger<ActivateToDoCommandHandler> logger) : IRequestHandler<ActivateToDoCommand, Response>
{
    public async ValueTask<Response> Handle(ActivateToDoCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ToDo>();
        var todo = await repository.GetAsync(request.Id, cancellationToken);
        if (todo is null)
        {
            logger.LogWarning("The todo with id {Id} was not found", request.Id);
            return new Response().NotFound();
        }

        todo.Activate(timeProvider.GetUtcNow().DateTime);
        repository.Update(todo);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("The todo with id {Id} was successfully activated", request.Id);

        return new Response().Success();
    }
}
