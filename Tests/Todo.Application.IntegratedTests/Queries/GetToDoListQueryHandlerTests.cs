using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Abstraction.Interfaces;
using Todo.Application.Queries.GetToDoList;
using Todo.Domain.Entities;

namespace Todo.Application.IntegratedTests.Queries;

public class GetToDoListQueryHandlerTests : IDisposable, IClassFixture<IntegratedFixture>
{
    private readonly IServiceScope _scope;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public GetToDoListQueryHandlerTests(IntegratedFixture fixture)
    {
        _scope = fixture.Configure().CreateAsyncScope();
        _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        _unitOfWork = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }

    [Fact]
    public async Task ShouldBeSuccessful()
    {
        var repository = _unitOfWork.Repository<ToDo>();
        repository.Add(new ToDo(Guid.NewGuid(), "Foo 1", "Bar 2", DateTime.UtcNow));
        repository.Add(new ToDo(Guid.NewGuid(), "Foo 2", "Bar 2", DateTime.UtcNow));
        await _unitOfWork.SaveChangesAsync();

        var result = await _mediator.Send(new GetToDoListQuery());

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.Ok);
        result.Data.ShouldNotBeNull();
        result.Data.Items.Count.ShouldBe(2);
    }

    [Fact]
    public async Task WithNotExistItem_ShouldReturnNotFound()
    {
        var result = await _mediator.Send(new GetToDoListQuery());

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.Ok);
        result.Data.ShouldNotBeNull();
        result.Data.Items.Count.ShouldBe(0);
    }

    public void Dispose()
    {
        _scope.Dispose();
    }
}
