using GymAppBackend.Core.Exceptions;
using GymAppBackend.Core.ValueObjects;
using GymAppBackend.Core.ValueObjects.Date;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Core.Workouts.Exceptions;

public sealed class WorkoutWithTheSameDateException : CustomException
{
    public Date WorkoutDate { get; }

    public WorkoutWithTheSameDateException(Date workoutDate)
        : base($"Workout on {workoutDate.Value.ToString()} already exists.", StatusCodes.Status400BadRequest)
    {
        WorkoutDate = workoutDate;
    }
}