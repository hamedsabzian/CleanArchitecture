using Todo.Application.Shared.Interfaces;
using Todo.Domain.Entities;

namespace Todo.Application.Commands.DoneToDo;

internal class DoneToDoCommandHandler(
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider,
    ILogger<DoneToDoCommandHandler> logger) : IRequestHandler<DoneToDoCommand, Response>
{
    public async ValueTask<Response> Handle(DoneToDoCommand request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.Repository<ToDo>();
        var todo = await repository.GetAsync(request.Id, cancellationToken);
        if (todo is null)
        {
            logger.LogWarning("The todo with id {Id} was not found", request.Id);
            return new Response().NotFound();
        }

        if (!todo.CanBeDone())
        {
            logger.LogWarning("The todo with id {Id} and status {Status} could not be done", request.Id, todo.Status);
            return new Response().Invalid().WithMessage("The item should be activated first");
        }

        todo.Done(timeProvider.GetUtcNow().DateTime);
        repository.Update(todo);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("The todo with id {Id} was successfully done", request.Id);

        return new Response().Success();
    }
}
