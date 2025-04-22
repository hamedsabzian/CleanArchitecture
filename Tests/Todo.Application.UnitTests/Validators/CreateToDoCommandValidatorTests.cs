using Todo.Application.Commands.CreateToDo;

namespace Todo.Application.UnitTests.Validators;

public class CreateToDoCommandValidatorTests
{
    [Theory]
    [InlineData("Foo", "Bar")]
    [InlineData("Foo", null)]
    [InlineData("Foo", "")]
    public void ShouldBeValid(string title, string? description)
    {
        var command = new CreateToDoCommand(title, description);
        var validator = new CreateToDoCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBe(true);
    }

    [Theory]
    [InlineData(null, "Foo")]
    [InlineData("", "Foo")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Foo")]
    public void ShouldBeInvalid(string title, string? description)
    {
        var command = new CreateToDoCommand(title, description);
        var validator = new CreateToDoCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBe(false);
    }

    [Fact]
    public void WithLargeDescription_ShouldBeInvalid()
    {
        var command = new CreateToDoCommand("Foo", new string('a', 1001));
        var validator = new CreateToDoCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBe(false);
    }
}
