using Mediator;
using Microsoft.Extensions.Logging;
using Moq;
using Todo.Application.Common.Behaviors;
using Todo.Application.Common.ResponseModels;

namespace Todo.Application.UnitTests.PipelineBehaviors;

public class ExceptionHandlingBehaviorTests
{
    [Fact]
    public async Task ShouldBeSuccessful()
    {
        var logger = new Mock<ILogger<ExceptionHandlingBehavior<FooRequest, Response>>>().Object;
        var behavior = new ExceptionHandlingBehavior<FooRequest, Response>(logger);

        var result = await behavior.Handle(
            new FooRequest(), CancellationToken.None, (_, _) => ValueTask.FromResult(new Response().Success()));

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.Ok);
    }

    [Fact]
    public async Task WithThrowException_ShouldReturnError()
    {
        var logger = new Mock<ILogger<ExceptionHandlingBehavior<FooRequest, Response>>>().Object;
        var behavior = new ExceptionHandlingBehavior<FooRequest, Response>(logger);

        var result = await behavior.Handle(
            new FooRequest(), CancellationToken.None, (_, _) => throw new InvalidOperationException());

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.Error);
    }

    public class FooRequest : IRequest<Response>;
}
