using Mediator;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Commands.ActivateToDo;
using Todo.Application.Commands.CreateToDo;
using Todo.Application.Commands.DeleteToDo;
using Todo.Application.Commands.DoneToDo;
using Todo.Application.Commands.UpdateToDo;
using Todo.Application.Queries.GetToDo;
using Todo.Application.Queries.GetToDoList;

namespace Todo.Api.Configurations;

public static class EndpointExtensions
{
    public static void MapToDoEndpoints(this WebApplication app)
    {
        app.MapGet("/todo", (
                    [FromServices] IMediator mediator,
                    CancellationToken cancellationToken,
                    [FromQuery] int pageNumber = 1,
                    [FromQuery] int pageSize = 10) =>
                mediator.Send(new GetToDoListQuery(pageNumber, pageSize), cancellationToken))
            .WithName("get-todo-list");

        app.MapGet("/todo/{id}",
                ([FromRoute] Guid id, [FromServices] IMediator mediator, CancellationToken cancellationToken) =>
                    mediator.Send(new GetToDoQuery(id), cancellationToken))
            .WithName("get-todo");

        app.MapPost("/todo",
                ([FromBody] CreateToDoCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken) =>
                    mediator.Send(command, cancellationToken))
            .WithName("create-todo");

        app.MapPut("/todo",
                ([FromBody] UpdateToDoCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken) =>
                    mediator.Send(command, cancellationToken))
            .WithName("update-todo");

        app.MapPut("/todo/activate",
                ([FromBody] ActivateToDoCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken) =>
                    mediator.Send(command, cancellationToken))
            .WithName("activate-todo");

        app.MapPut("/todo/done",
                ([FromBody] DoneToDoCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken) =>
                    mediator.Send(command, cancellationToken))
            .WithName("done-todo");

        app.MapDelete("/todo/{id}",
                ([FromRoute] Guid id, [FromServices] IMediator mediator, CancellationToken cancellationToken) =>
                    mediator.Send(new DeleteToDoCommand(id), cancellationToken))
            .WithName("delete-todo");
    }
}
