using FluentValidation;

namespace Todo.Application.Queries.GetToDo;

public class GetToDoQueryValidator : AbstractValidator<GetToDoQuery>
{
    public GetToDoQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
