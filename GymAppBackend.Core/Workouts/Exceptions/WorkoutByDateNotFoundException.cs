using GymAppBackend.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Core.Workouts.Exceptions;

public sealed class WorkoutByDateNotFoundException : CustomException
{
    public DateTimeOffset Date { get; }

    public WorkoutByDateNotFoundException(DateTimeOffset date) : base($"Workout with date {date.ToString()} does not exist.", StatusCodes.Status404NotFound)
    {
        Date = date;
    }
}