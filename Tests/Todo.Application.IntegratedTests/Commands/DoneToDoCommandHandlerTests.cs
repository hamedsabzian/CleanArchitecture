using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Commands.DoneToDo;
using Todo.Application.Shared.Interfaces;
using Todo.Domain.Entities;
using Todo.Domain.Enums;

namespace Todo.Application.IntegratedTests.Commands;

public class DoneToDoCommandHandlerTests : IDisposable, IClassFixture<IntegratedFixture>
{
    private readonly IServiceScope _scope;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public DoneToDoCommandHandlerTests(IntegratedFixture fixture)
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
        todo.Activate(DateTime.UtcNow);
        repository.Add(todo);
        await _unitOfWork.SaveChangesAsync();
        var command = new DoneToDoCommand(todo.Id);

        var result = await _mediator.Send(command);

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.Ok);
        var existingTodo = await repository.GetAsync(todo.Id);
        existingTodo.ShouldNotBeNull();
        existingTodo.Status.ShouldBe(ToDoStatus.Done);
    }

    [Fact]
    public async Task WithNotActivatedItem_ShouldBeFailed()
    {
        var repository = _unitOfWork.Repository<ToDo>();
        var todo = new ToDo(Guid.NewGuid(), "Foo", "Bar", DateTime.UtcNow);
        repository.Add(todo);
        await _unitOfWork.SaveChangesAsync();
        var command = new DoneToDoCommand(todo.Id);

        var result = await _mediator.Send(command);

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.Invalid);
        var existingTodo = await repository.GetAsync(todo.Id);
        existingTodo.ShouldNotBeNull();
        existingTodo.Status.ShouldBe(ToDoStatus.Created);
    }

    [Fact]
    public async Task WithNotExistItem_ShouldBeFailed()
    {
        var command = new DoneToDoCommand(Guid.NewGuid());

        var result = await _mediator.Send(command);

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.NotFound);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
