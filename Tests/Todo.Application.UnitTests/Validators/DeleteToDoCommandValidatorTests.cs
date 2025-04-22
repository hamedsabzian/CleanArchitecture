using Todo.Application.Commands.DeleteToDo;

namespace Todo.Application.UnitTests.Validators;

public class DeleteToDoCommandValidatorTests
{
    [Fact]
    public void ShouldBeValid()
    {
        var command = new DeleteToDoCommand(Guid.NewGuid());
        var validator = new DeleteToDoCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBe(true);
    }

    [Fact]
    public void WithEmptyId_ShouldBeValid()
    {
        var command = new DeleteToDoCommand(Guid.Empty);
        var validator = new DeleteToDoCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBe(false);
    }
}
