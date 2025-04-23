using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Abstraction.Interfaces;
using Todo.Application.Commands.CreateToDo;
using Todo.Domain.Entities;
using Todo.Domain.Enums;

namespace Todo.Application.IntegratedTests.Commands;

public class CreateToDoCommandHandlerTests : IDisposable, IClassFixture<IntegratedFixture>
{
    private readonly IServiceScope _scope;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateToDoCommandHandlerTests(IntegratedFixture fixture)
    {
        _scope = fixture.Configure().CreateAsyncScope();
        _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        _unitOfWork = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    [Fact]
    public async Task ShouldBeSuccessful()
    {
        var command = new CreateToDoCommand("Foo", "Bar");

        var result = await _mediator.Send(command);

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.Ok);
        var todo = await _unitOfWork.Repository<ToDo>().GetAsync(result.Data!.Value);
        todo!.Title.ShouldBe(command.Title);
        todo.Description.ShouldBe(command.Description);
        todo.Status.ShouldBe(ToDoStatus.Created);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
