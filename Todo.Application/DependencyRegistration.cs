using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Shared.Interfaces;
using Todo.Application.Commands.CreateToDo;
using Todo.Application.Common;
using Todo.Application.Common.Behaviors;
using Todo.Application.Events.Common;

namespace Todo.Application;

public static class DependencyRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services, bool addPipelineBehavior = true)
    {
        services.AddValidatorsFromAssemblyContaining<CreateToDoCommandValidator>();

        services.AddScoped<IDomainEventDispatcher, MediatorDomainEventDispatcher>();
        services.AddSingleton<IIdGenerator, IdGenerator>();
        services.AddSingleton<TimeProvider>(TimeProvider.System);

        services.AddMediator(options => { options.ServiceLifetime = ServiceLifetime.Scoped; });

        if (addPipelineBehavior)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        return services;
    }
}
