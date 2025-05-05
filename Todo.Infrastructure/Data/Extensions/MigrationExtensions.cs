using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.Infrastructure.Data.Extensions;

public static class MigrationExtensions
{
    public static async Task MigrateDatabase(this IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
        await db.Database.MigrateAsync();
    }
}
