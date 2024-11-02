using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Workouts.Queries.DTO;

namespace GymAppBackend.Application.Workouts.Queries.GetWorkout;

public class GetWorkoutQuery : IQuery<WorkoutDetailsDto>
{
    public Guid Id { get; set; }
}