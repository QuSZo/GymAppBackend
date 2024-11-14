using GymAppBackend.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace GymAppBackend.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssemblyWithResult = typeof(ICommandHandler<,>).Assembly;
        var applicationAssemblyWithoutResult = typeof(ICommandHandler<>).Assembly;

        services.Scan(s => s.FromAssemblies(applicationAssemblyWithResult)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(s => s.FromAssemblies(applicationAssemblyWithoutResult)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}