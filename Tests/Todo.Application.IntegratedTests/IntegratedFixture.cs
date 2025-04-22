using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Abstraction.Interfaces;
using Todo.Infrastructure.Data;

namespace Todo.Application.IntegratedTests;

public class IntegratedFixture
{
    public IServiceProvider Configure()
    {
        return Configure(_ => { });
    }
    public IServiceProvider Configure(Action<IServiceCollection> configureServices)
    {
        var services = new ServiceCollection();
        services.AddLogging();

        services.AddApplication(false);

        services.AddDbContext<ToDoDbContext>(builder => { builder.UseInMemoryDatabase(Guid.NewGuid().ToString()); });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        configureServices(services);

        return services.BuildServiceProvider();
    }
}
