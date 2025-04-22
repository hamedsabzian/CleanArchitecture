using FluentValidation;

namespace Todo.Application.Commands.UpdateToDo;

public class UpdateToDoCommandValidator : AbstractValidator<UpdateToDoCommand>
{
    public UpdateToDoCommandValidator()
    {
        RuleFor(a => a.Id).NotEmpty();

        RuleFor(a => a.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(a => a.Description).MaximumLength(1000);
    }
}
