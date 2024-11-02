using GymAppBackend.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Core.Workouts.Exceptions;

public sealed class WorkoutNotFoundException : CustomException
{
    public Guid WorkoutId { get; }
    public WorkoutNotFoundException(Guid workoutId) : base($"Workout with id {workoutId} does not exist.", StatusCodes.Status404NotFound)
    {
        WorkoutId = workoutId;
    }
}