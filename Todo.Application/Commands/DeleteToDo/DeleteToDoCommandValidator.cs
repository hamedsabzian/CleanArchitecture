using FluentValidation;

namespace Todo.Application.Commands.DeleteToDo;

public class DeleteToDoCommandValidator : AbstractValidator<DeleteToDoCommand>
{
    public DeleteToDoCommandValidator()
    {
        RuleFor(a => a.Id).NotEmpty();
    }
}
