using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Workouts.Queries.DTO;

namespace GymAppBackend.Application.Workouts.Queries.GetWorkoutByDate;

public class GetWorkoutByDateQuery : IQuery<WorkoutDetailsDto>
{
    public DateTimeOffset Date { get; set; }
}