using FluentValidation;

namespace Todo.Application.Commands.ActivateToDo;

internal class ActivateToDoCommandValidator : AbstractValidator<ActivateToDoCommand>
{
    public ActivateToDoCommandValidator()
    {
        RuleFor(a => a.Id).NotEmpty();
    }
}
