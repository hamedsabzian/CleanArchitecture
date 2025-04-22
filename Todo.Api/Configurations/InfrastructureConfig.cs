using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Data;

namespace Todo.Api.Configurations;

internal static class InfrastructureConfig
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ToDoDbContext>(builder =>
        {
            builder.UseSqlServer(configuration.GetConnectionString("ToDoDatabase"));
        });
        return services;
    }
}