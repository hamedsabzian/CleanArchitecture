using FluentValidation;

namespace Todo.Application.Commands.CreateToDo;

internal class CreateToDoCommandValidator : AbstractValidator<CreateToDoCommand>
{
    public CreateToDoCommandValidator()
    {
        RuleFor(a => a.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(a => a.Description).MaximumLength(1000);
    }
}
