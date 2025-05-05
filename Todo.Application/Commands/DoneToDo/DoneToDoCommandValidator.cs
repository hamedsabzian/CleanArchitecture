using FluentValidation;

namespace Todo.Application.Commands.DoneToDo;

internal class DoneToDoCommandValidator : AbstractValidator<DoneToDoCommand>
{
    public DoneToDoCommandValidator()
    {
        RuleFor(a => a.Id).NotEmpty();
    }
}
