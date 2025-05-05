using Todo.Application.Shared.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.UpdateToDo;

internal class UpdateToDoCommandHandler(
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider,
    ILogger<UpdateToDoCommandHandler> logger) : IRequestHandler<UpdateToDoCommand, Response>
{
    public async ValueTask<Response> Handle(UpdateToDoCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ToDo>();
        var todo = await repository.GetAsync(request.Id, cancellationToken);
        if (todo is null)
        {
            logger.LogWarning("The todo with id {Id} was not found", request.Id);
            return new Response().NotFound();
        }

        todo.Update(request.Title, request.Description, timeProvider.GetUtcNow().DateTime);
        repository.Update(todo);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("The todo with id {Id} was successfully updated", request.Id);

        return new Response().Success();
    }
}
