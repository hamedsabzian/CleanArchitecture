using Todo.Application.Queries.GetToDoList;

namespace Todo.Application.UnitTests.Validators;

public class GetToDoListQueryValidatorTests
{
    [Fact]
    public void ShouldBeValid()
    {
        var query = new GetToDoListQuery(2, 20);
        var validator = new GetToDoListQueryValidator();

        var result = validator.Validate(query);

        result.IsValid.ShouldBe(true);
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(-1, 10)]
    [InlineData(1, 0)]
    [InlineData(1, -1)]
    [InlineData(1, 2000)]
    public void WithEmptyId_ShouldBeValid(int pageNumber, int pageSize)
    {
        var query = new GetToDoListQuery(pageNumber, pageSize);
        var validator = new GetToDoListQueryValidator();

        var result = validator.Validate(query);

        result.IsValid.ShouldBe(false);
    }
}
