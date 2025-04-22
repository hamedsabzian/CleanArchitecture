using FluentValidation;
using Mediator;
using Microsoft.Extensions.Logging;
using Moq;
using Todo.Application.Common.Behaviors;
using Todo.Application.Common.ResponseModels;

namespace Todo.Application.UnitTests.PipelineBehaviors;

public class ValidationBehaviorTests
{
    [Fact]
    public async Task ShouldBeSuccessful()
    {
        var logger = new Mock<ILogger<ValidationBehavior<FooRequest, Response>>>().Object;
        var behavior = new ValidationBehavior<FooRequest, Response>([new FooRequestValidator()], logger);

        var result = await behavior.Handle(
            new FooRequest(Guid.NewGuid()), CancellationToken.None, (_, _) => ValueTask.FromResult(new Response().Success()));

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.Ok);
    }

    [Fact]
    public async Task WithNoValidator_ShouldBeSuccessful()
    {
        var logger = new Mock<ILogger<ValidationBehavior<FooRequest, Response>>>().Object;
        var behavior = new ValidationBehavior<FooRequest, Response>([], logger);

        var result = await behavior.Handle(
            new FooRequest(Guid.NewGuid()), CancellationToken.None, (_, _) => ValueTask.FromResult(new Response().Success()));

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.Ok);
    }

    [Fact]
    public async Task WithInvalidRequest_ShouldReturnInvalid()
    {
        var logger = new Mock<ILogger<ValidationBehavior<FooRequest, Response>>>().Object;
        var behavior = new ValidationBehavior<FooRequest, Response>([new FooRequestValidator()], logger);

        var result = await behavior.Handle(
            new FooRequest(Guid.Empty), CancellationToken.None, (_, _) => throw new InvalidOperationException());

        result.StatusCode.ShouldBe(DefaultResponseStatusCodes.Invalid);
    }

    public record FooRequest(Guid Id) : IRequest<Response>;

    class FooRequestValidator : AbstractValidator<FooRequest>
    {
        public FooRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
