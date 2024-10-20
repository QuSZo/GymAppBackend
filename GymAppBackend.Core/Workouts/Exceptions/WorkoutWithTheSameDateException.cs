using GymAppBackend.Core.Exceptions;
using GymAppBackend.Core.ValueObjects;

namespace GymAppBackend.Core.Workouts.Exceptions;

public sealed class WorkoutWithTheSameDateException : CustomException
{
    public Date WorkoutDate { get; }

    public WorkoutWithTheSameDateException(Date workoutDate, int statusCode)
        : base($"Workout on {workoutDate.Value.ToString()} already exists.", statusCode)
    {
        WorkoutDate = workoutDate;
    }
}