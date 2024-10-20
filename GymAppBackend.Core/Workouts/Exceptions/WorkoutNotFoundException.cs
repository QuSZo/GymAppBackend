using GymAppBackend.Core.Exceptions;
using GymAppBackend.Core.ValueObjects;

namespace GymAppBackend.Core.Workouts.Exceptions;

public sealed class WorkoutNotFoundException : CustomException
{
    public Date Date { get; set; }

    public WorkoutNotFoundException(Date date, int statusCode) : base($"No workout found on {date.Value.Date}.", statusCode)
    {
        Date = date;
    }
}