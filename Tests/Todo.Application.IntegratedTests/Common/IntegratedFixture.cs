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

        services.AddDbContext<ToDoDbContext>(builder => { builder.UseInMemoryDatabase(Guid.NewGuid().ToString()); });

        configureServices(services);

        return services.BuildServiceProvider();
    }
}
