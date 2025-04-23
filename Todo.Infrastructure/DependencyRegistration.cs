using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Abstraction.Interfaces;
using Todo.Infrastructure.Data;
using Todo.Infrastructure.Data.Readers;

namespace Todo.Infrastructure;

public static class DependencyRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddToDoDbContext(configuration)
            .AddDataDependencies();
    }

    public static IServiceCollection AddToDoDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ToDoDbContext>(builder => { builder.UseSqlServer(configuration.GetConnectionString("ToDoDatabase")); });

        return services;
    }

    public static IServiceCollection AddDataDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IToDoReader, ToDoReader>();

        return services;
    }
}
