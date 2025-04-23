using FluentValidation;

namespace Todo.Application.Commands.DoneToDo;

public class DoneToDoCommandValidator : AbstractValidator<DoneToDoCommand>
{
    public DoneToDoCommandValidator()
    {
        RuleFor(a => a.Id).NotEmpty();
    }
}
