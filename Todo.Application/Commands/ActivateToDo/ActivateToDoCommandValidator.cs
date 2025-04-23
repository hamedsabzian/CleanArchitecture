using FluentValidation;

namespace Todo.Application.Commands.ActivateToDo;

public class ActivateToDoCommandValidator : AbstractValidator<ActivateToDoCommand>
{
    public ActivateToDoCommandValidator()
    {
        RuleFor(a => a.Id).NotEmpty();
    }
}
