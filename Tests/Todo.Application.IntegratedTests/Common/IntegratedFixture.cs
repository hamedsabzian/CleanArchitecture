using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.Infrastructure;
using Todo.Infrastructure.Data;

namespace Todo.Application.IntegratedTests.Common;

public sealed class IntegratedFixture
{
    public IServiceProvider Configure()
    {
        return Configure(_ => { });
    }

    public IServiceProvider Configure(Action<IServiceCollection> configureServices)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddMemoryCache();

        services.AddApplication(false)
            .AddDataDependencies();

        services.AddDbContext<ToDoTestDbContext>(builder => { builder.UseInMemoryDatabase(Guid.NewGuid().ToString()); });
        services.AddScoped<ToDoDbContext>(provider =>  provider.GetRequiredService<ToDoTestDbContext>());

        configureServices(services);

        return services.BuildServiceProvider();
    }
}
