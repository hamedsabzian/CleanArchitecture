using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Abstraction.Interfaces;
using Todo.Application.Queries.GetToDo;
using Todo.Domain.Entities;
using Todo.Domain.Enums;

namespace Todo.Application.IntegratedTests.Queries;

public class GetToDoQueryHandlerTests : IDisposable, IClassFixture<IntegratedFixture>
{
    private readonly IServiceScope _scope;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public GetToDoQueryHandlerTests(IntegratedFixture fixture)
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

        var result = await _mediator.Send(new GetToDoQuery(todo.Id));

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.Ok);
        result.Data.ShouldNotBeNull();
        result.Data.Title.ShouldBe("Foo");
        result.Data.Description.ShouldBe("Bar");
        result.Data.Status.ShouldBe(ToDoStatus.Created);
    }

    [Fact]
    public async Task WithNotExistItem_ShouldReturnNotFound()
    {
        var result = await _mediator.Send(new GetToDoQuery(Guid.NewGuid()));

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.NotFound);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
