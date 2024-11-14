using GymAppBackend.Core.ExerciseCategories.Entities;
using GymAppBackend.Core.Exercises.Entities;
using GymAppBackend.Core.ExerciseSets.Entities;
using GymAppBackend.Core.ExerciseTypes.Entities;
using GymAppBackend.Core.PasswordResetTokens.Entities;
using GymAppBackend.Core.Users.Entities;
using GymAppBackend.Core.Workouts.Entities;
using GymAppBackend.Infrastructure.DAL.Users.Configurations;
using GymAppBackend.Infrastructure.DAL.Workouts.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL;

internal sealed class GymAppDbContext : DbContext
{
    public DbSet<ExerciseCategory> ExerciseCategories { get; set; }
    public DbSet<ExerciseType> ExerciseTypes { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<ExerciseSet> ExerciseSets { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

    public GymAppDbContext(DbContextOptions<GymAppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WorkoutConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}