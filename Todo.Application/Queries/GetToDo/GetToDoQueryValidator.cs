using FluentValidation;

namespace Todo.Application.Queries.GetToDo;

internal class GetToDoQueryValidator : AbstractValidator<GetToDoQuery>
{
    public GetToDoQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
