using Mediator;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Commands.CreateToDo;

namespace Todo.Api.Configurations;

public static class EndpointExtensions
{
    public static void MapToDoEndpoints(this WebApplication app)
    {
        app.MapPost("/todo",
                ([FromBody] CreateToDoCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken) =>
                    mediator.Send(command, cancellationToken))
            .WithName("create-todo");
    }
}
