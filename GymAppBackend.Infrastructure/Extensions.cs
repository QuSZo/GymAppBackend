using GymAppBackend.Application.Abstractions;
using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.Repositories;
using GymAppBackend.Infrastructure.DAL.Workouts.Repositories;
using GymAppBackend.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GymAppBackend.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddControllers();

        services
            .AddSingleton<IWorkoutRepository, InMemoryWorkoutRepository>()
            .AddSingleton<IClock, Clock>();

        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "GymApp API",
                Version = "v1"
            });
        });

        var infrastructureAssembly = typeof(AppOptions).Assembly;

        services.Scan(s => s.FromAssemblies(infrastructureAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();

        return app;
    }
}