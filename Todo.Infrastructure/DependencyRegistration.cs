using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Abstraction.Interfaces;
using Todo.Infrastructure.Data;

namespace Todo.Infrastructure;

public static class DependencyRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ToDoDbContext>(builder => { builder.UseSqlServer(configuration.GetConnectionString("ToDoDatabase")); });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
