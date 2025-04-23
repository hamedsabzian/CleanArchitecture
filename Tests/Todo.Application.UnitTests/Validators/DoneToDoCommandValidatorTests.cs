using Todo.Application.Commands.DoneToDo;

namespace Todo.Application.UnitTests.Validators;

public class DoneToDoCommandValidatorTests
{
    [Fact]
    public void ShouldBeValid()
    {
        var command = new DoneToDoCommand(Guid.NewGuid());
        var validator = new DoneToDoCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBe(true);
    }

    [Fact]
    public void WithEmptyId_ShouldBeValid()
    {
        var command = new DoneToDoCommand(Guid.Empty);
        var validator = new DoneToDoCommandValidator();

        var result = validator.Validate(command);

        result.IsValid.ShouldBe(false);
    }
}
