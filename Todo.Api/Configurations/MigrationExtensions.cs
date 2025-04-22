using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Data;

namespace Todo.Api.Configurations;

public static class MigrationExtensions
{
    public static async Task MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
        await db.Database.MigrateAsync();
    }
}
