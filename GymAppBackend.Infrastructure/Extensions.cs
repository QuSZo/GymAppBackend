using GymAppBackend.Application.Abstractions;
using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ExerciseCategories.Repositories;
using GymAppBackend.Core.ExerciseTypes.Repositories;
using GymAppBackend.Core.Workouts.Repositories;
using GymAppBackend.Infrastructure.DAL.ExerciseCategories.Repositories;
using GymAppBackend.Infrastructure.DAL.ExerciseTypes.Repositories;
using GymAppBackend.Infrastructure.DAL.Workouts.Repositories;
using GymAppBackend.Infrastructure.Exceptions;
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
        services.AddSingleton<ExceptionMiddleware>();

        services
            .AddSingleton<IWorkoutRepository, InMemoryWorkoutRepository>()
            .AddSingleton<IExerciseTypeRepository, InMemoryExerciseTypeRepository>()
            .AddSingleton<IExerciseCategoryRepository, InMemoryExerciseCategoryRepository>()
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
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();

        return app;
    }
}