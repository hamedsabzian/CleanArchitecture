using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Commands.CreateToDo;
using Todo.Application.Common;
using Todo.Application.Common.Behaviors;

namespace Todo.Application;

public static class DependencyRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateToDoCommandValidator>();

        services.AddSingleton<IIdGenerator, IdGenerator>();
        services.AddSingleton<TimeProvider>(TimeProvider.System);

        services.AddMediator(options => { options.ServiceLifetime = ServiceLifetime.Scoped; });
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}
