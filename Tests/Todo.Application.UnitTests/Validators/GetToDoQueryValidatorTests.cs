using Todo.Application.Queries.GetToDo;

namespace Todo.Application.UnitTests.Validators;

public class GetToDoQueryValidatorTests
{
    [Fact]
    public void ShouldBeValid()
    {
        var query = new GetToDoQuery(Guid.NewGuid());
        var validator = new GetToDoQueryValidator();

        var result = validator.Validate(query);

        result.IsValid.ShouldBe(true);
    }

    [Fact]
    public void WithEmptyId_ShouldBeValid()
    {
        var query = new GetToDoQuery(Guid.Empty);
        var validator = new GetToDoQueryValidator();

        var result = validator.Validate(query);

        result.IsValid.ShouldBe(false);
    }
}
