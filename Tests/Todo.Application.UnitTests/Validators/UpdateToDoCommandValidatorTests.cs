using Todo.Application.Commands.UpdateToDo;

namespace Todo.Application.UnitTests.Validators;

public class UpdateToDoCommandValidatorTests
{
    [Theory]
    [InlineData("Foo", "Bar")]
    [InlineData("Foo", null)]
    [InlineData("Foo", "")]
    public void ShouldBeValid(string title, string? description)
    {
        var command = new UpdateToDoCommand(Guid.NewGuid(), title, description);
        var validator = new UpdateToDoCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBe(true);
    }

    [Theory]
    [InlineData("e05a4d11-0a19-4fdf-b2aa-998a53758480", null, "Foo")]
    [InlineData("e05a4d11-0a19-4fdf-b2aa-998a53758480", "", "Foo")]
    [InlineData("e05a4d11-0a19-4fdf-b2aa-998a53758480",
        "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Foo")]
    [InlineData("00000000-0000-0000-0000-000000000000", "Foo", "Bar")]
    public void ShouldBeInvalid(string id, string title, string? description)
    {
        var command = new UpdateToDoCommand(Guid.Parse(id), title, description);
        var validator = new UpdateToDoCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBe(false);
    }

    [Fact]
    public void WithLargeDescription_ShouldBeInvalid()
    {
        var command = new UpdateToDoCommand(Guid.NewGuid(), "Foo", new string('a', 1001));
        var validator = new UpdateToDoCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBe(false);
    }
}
