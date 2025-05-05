using FluentValidation;

namespace Todo.Application.Queries.GetToDoList;

internal class GetToDoListQueryValidator : AbstractValidator<GetToDoListQuery>
{
    public GetToDoListQueryValidator()
    {
        RuleFor(a => a.PageNumber).GreaterThan(0);

        RuleFor(a => a.PageSize).GreaterThan(0).LessThan(100);
    }
}
