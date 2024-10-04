using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Workouts.Queries.DTO;

namespace GymAppBackend.Application.Workouts.Queries.GetWorkouts;

public class GetWorkoutsQuery : IQuery<IEnumerable<WorkoutsDto>>
{
    public DateTimeOffset? Date { get; set; }
}