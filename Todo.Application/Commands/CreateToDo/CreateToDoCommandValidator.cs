using FluentValidation;

namespace Todo.Application.Commands.CreateToDo;

public class CreateToDoCommandValidator : AbstractValidator<CreateToDoCommand>
{
    public CreateToDoCommandValidator()
    {
        RuleFor(a => a.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(a => a.Description).MaximumLength(1000);
    }
}
