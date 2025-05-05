using Scalar.AspNetCore;
using Todo.Api.Configurations;
using Todo.Application;
using Todo.Application.Common.ResponseModels;
using Todo.Infrastructure;
using Todo.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddApplication()
    .AddInfrastructure(builder.Configuration);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

await app.Services.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            await Results.Json(new Response().Error().WithMessage("An exception has occurred")).ExecuteAsync(context);
        });
    });
}

app.UseHttpsRedirection();

app.MapGet("/", () => "TODO App")
    .WithName("Home");

app.MapToDoEndpoints();

app.Run();
