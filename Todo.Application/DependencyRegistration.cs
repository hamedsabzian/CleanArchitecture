using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Common;

namespace Todo.Application;

public static class DependencyRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IIdGenerator, IdGenerator>();
        services.AddSingleton<TimeProvider>(TimeProvider.System);

        services.AddMediator(options => { options.ServiceLifetime = ServiceLifetime.Scoped; });
        return services;
    }
}
