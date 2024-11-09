using GymAppBackend.Core.ExerciseCategories.Repositories;
using GymAppBackend.Core.Exercises.Repositories;
using GymAppBackend.Core.ExerciseSets.Repositories;
using GymAppBackend.Core.ExerciseTypes.Repositories;
using GymAppBackend.Core.Users.Repositories;
using GymAppBackend.Core.Workouts.Repositories;
using GymAppBackend.Infrastructure.DAL.ExerciseCategories.Repositories;
using GymAppBackend.Infrastructure.DAL.Exercises.Repositories;
using GymAppBackend.Infrastructure.DAL.ExerciseSets.Repositories;
using GymAppBackend.Infrastructure.DAL.ExerciseTypes.Repositories;
using GymAppBackend.Infrastructure.DAL.Users.Repositories;
using GymAppBackend.Infrastructure.DAL.Workouts.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GymAppBackend.Infrastructure.DAL;

internal static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        const string connectionString = "Host=localhost;Database=GymApp;Username=postgres;Password=admin;";
        services.AddDbContext<GymAppDbContext>(options => options.UseNpgsql(connectionString));

        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IWorkoutRepository, WorkoutRepository>()
            .AddScoped<IExerciseTypeRepository, ExerciseTypeRepository>()
            .AddScoped<IExerciseCategoryRepository, ExerciseCategoryRepository>()
            .AddScoped<IExerciseRepository, ExerciseRepository>()
            .AddScoped<IExerciseSetRepository, ExerciseSetRepository>();

        services.AddHostedService<DatabaseInitializer>();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}