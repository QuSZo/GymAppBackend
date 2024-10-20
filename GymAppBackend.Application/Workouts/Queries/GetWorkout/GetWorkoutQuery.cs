using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Core.ValueObjects;

namespace GymAppBackend.Application.Workouts.Queries.GetWorkout;

public class GetWorkoutQuery : IQuery<WorkoutDetailsDto>
{
    public Date Date { get; set; }
}