using FluentValidation;

namespace Todo.Application.Commands.DeleteToDo;

internal class DeleteToDoCommandValidator : AbstractValidator<DeleteToDoCommand>
{
    public DeleteToDoCommandValidator()
    {
        RuleFor(a => a.Id).NotEmpty();
    }
}
