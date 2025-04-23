using Todo.Application.Commands.ActivateToDo;

namespace Todo.Application.UnitTests.Validators;

public class ActivateToDoCommandValidatorTests
{
    [Fact]
    public void ShouldBeValid()
    {
        var command = new ActivateToDoCommand(Guid.NewGuid());
        var validator = new ActivateToDoCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBe(true);
    }

    [Fact]
    public void WithEmptyId_ShouldBeValid()
    {
        var command = new ActivateToDoCommand(Guid.Empty);
        var validator = new ActivateToDoCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBe(false);
    }
}
