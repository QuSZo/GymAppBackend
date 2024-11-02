using GymAppBackend.Core.ValueObjects;
using GymAppBackend.Core.Workouts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymAppBackend.Infrastructure.DAL.Workouts.Configurations;

internal sealed class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.Property(workout => workout.Date)
            .HasConversion(date => date.Value, date => new Date(date));
    }
}