using GymAppBackend.Application.Abstractions;
using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ExerciseCategories.Repositories;
using GymAppBackend.Core.Exercises.Repositories;
using GymAppBackend.Core.ExerciseTypes.Repositories;
using GymAppBackend.Core.Workouts.Repositories;
using GymAppBackend.Infrastructure.Auth;
using GymAppBackend.Infrastructure.DAL;
using GymAppBackend.Infrastructure.DAL.ExerciseCategories.Repositories;
using GymAppBackend.Infrastructure.DAL.Exercises.Repositories;
using GymAppBackend.Infrastructure.DAL.ExerciseTypes.Repositories;
using GymAppBackend.Infrastructure.DAL.Workouts.Repositories;
using GymAppBackend.Infrastructure.Exceptions;
using GymAppBackend.Infrastructure.Security;
using GymAppBackend.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace GymAppBackend.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy
                        .WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

        services.AddControllers();
        services.AddSingleton<ExceptionMiddleware>();
        services.AddHttpContextAccessor();
        services.AddSecurity();
        services.AddAuth(configuration);

        services
            .AddPostgres()
            // .AddSingleton<IWorkoutRepository, InMemoryWorkoutRepository>()
            // .AddSingleton<IExerciseTypeRepository, InMemoryExerciseTypeRepository>()
            // .AddSingleton<IExerciseCategoryRepository, InMemoryExerciseCategoryRepository>()
            // .AddSingleton<IExerciseRepository, InMemoryExerciseRepository>()
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
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();

        return app;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}