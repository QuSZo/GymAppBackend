using GymAppBackend.Core.ExerciseCategories.Repositories;
using GymAppBackend.Core.Exercises.Repositories;
using GymAppBackend.Core.ExerciseSets.Repositories;
using GymAppBackend.Core.ExerciseTypes.Repositories;
using GymAppBackend.Core.PasswordResetTokens.Repositories;
using GymAppBackend.Core.Users.Repositories;
using GymAppBackend.Core.Workouts.Repositories;
using GymAppBackend.Infrastructure.DAL.ExerciseCategories.Repositories;
using GymAppBackend.Infrastructure.DAL.Exercises.Repositories;
using GymAppBackend.Infrastructure.DAL.ExerciseSets.Repositories;
using GymAppBackend.Infrastructure.DAL.ExerciseTypes.Repositories;
using GymAppBackend.Infrastructure.DAL.PasswordResetTokens.Repositories;
using GymAppBackend.Infrastructure.DAL.Users.Repositories;
using GymAppBackend.Infrastructure.DAL.Workouts.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymAppBackend.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "postgres";

    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(OptionsSectionName);
        services.AddDbContext<GymAppDbContext>(options => options.UseNpgsql(postgresOptions.ConnectionString));

        services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IPasswordResetTokenRepository, PasswordResetTokenRepository>()
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