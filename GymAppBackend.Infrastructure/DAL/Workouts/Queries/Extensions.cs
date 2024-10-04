using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Core.Entities;

namespace GymAppBackend.Infrastructure.DAL.Workouts.Queries;

public static class Extensions
{
    public static WorkoutsDto AsDto(this Workout workout)
    {
        return new()
        {
            Id = workout.Id,
            Name = workout.Name,
            Date = workout.Date,
        };
    }
}