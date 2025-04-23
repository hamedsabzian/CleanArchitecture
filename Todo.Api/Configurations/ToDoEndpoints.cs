using Mediator;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Commands.CreateToDo;
using Todo.Application.Commands.DeleteToDo;
using Todo.Application.Commands.UpdateToDo;
using Todo.Application.Queries.GetToDo;

namespace Todo.Api.Configurations;

public static class EndpointExtensions
{
    public static void MapToDoEndpoints(this WebApplication app)
    {
        app.MapGet("/todo/{id}",
                ([FromRoute] Guid id, [FromServices] IMediator mediator, CancellationToken cancellationToken) =>
                    mediator.Send(new GetToDoQuery(id), cancellationToken))
            .WithName("get-todo");

        app.MapPost("/todo",
                ([FromBody] CreateToDoCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken) =>
                    mediator.Send(command, cancellationToken))
            .WithName("create-todo");

        app.MapDelete("/todo/{id}",
                ([FromRoute] Guid id, [FromServices] IMediator mediator, CancellationToken cancellationToken) =>
                    mediator.Send(new DeleteToDoCommand(id), cancellationToken))
            .WithName("delete-todo");

        app.MapPut("/todo",
                ([FromBody] UpdateToDoCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken) =>
                    mediator.Send(command, cancellationToken))
            .WithName("update-todo");
    }
}
