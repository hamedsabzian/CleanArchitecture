using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Abstraction.Interfaces;
using Todo.Application.Commands.DeleteToDo;
using Todo.Application.Commands.UpdateToDo;
using Todo.Domain.Entities;

namespace Todo.Application.IntegratedTests.Commands;

public class UpdateToDoCommandHandlerTests : IDisposable, IClassFixture<IntegratedFixture>
{
    private readonly IServiceScope _scope;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateToDoCommandHandlerTests(IntegratedFixture fixture)
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
        await _unitOfWork.SaveChangesAsync();
        var command = new UpdateToDoCommand(todo.Id, "Updated Foo", "Updated Bar");

        var result = await _mediator.Send(command);

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.Ok);
        var existingTodo = await repository.GetAsync(todo.Id);
        existingTodo.ShouldNotBeNull();
        existingTodo.Title.ShouldBe("Updated Foo");
        existingTodo.Description.ShouldBe("Updated Bar");
    }

    [Fact]
    public async Task WithNotExistItem_ShouldBeFailed()
    {
        var command = new DeleteToDoCommand(Guid.NewGuid());

        var result = await _mediator.Send(command);

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.NotFound);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
