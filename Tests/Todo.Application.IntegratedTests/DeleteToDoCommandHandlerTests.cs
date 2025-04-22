using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Abstraction.Interfaces;
using Todo.Application.Commands.DeleteToDo;
using Todo.Domain.Entities;

namespace Todo.Application.IntegratedTests;

public class DeleteToDoCommandHandlerTests : IDisposable, IClassFixture<IntegratedFixture>
{
    private readonly IServiceScope _scope;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteToDoCommandHandlerTests(IntegratedFixture fixture)
    {
        _scope = fixture.Configure().CreateAsyncScope();
        _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        _unitOfWork = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    [Fact]
    public async Task ShouldBeSuccessful()
    {
        var repository = _unitOfWork.Repository<ToDo>();
        var todo = new ToDo(Guid.NewGuid(), "Foo", "Bar", DateTime.UtcNow);
        repository.Add(todo);
        var command = new DeleteToDoCommand(todo.Id);

        var result = await _mediator.Send(command);

        result.StatusCode.ShouldBe(200);
        var existingTodo = await repository.GetAsync(todo.Id);
        existingTodo.ShouldBeNull();
    }

    [Fact]
    public async Task WithNotExistItem_ShouldBeFailed()
    {
        var command = new DeleteToDoCommand(Guid.NewGuid());

        var result = await _mediator.Send(command);

        result.StatusCode.ShouldBe(404);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
