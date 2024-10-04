using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Queries.Workouts.DTO;

namespace GymAppBackend.Application.Queries.Workouts.GetWorkouts;

public class GetWorkoutsQuery : IQuery<IEnumerable<WorkoutsDto>>
{
    public DateTimeOffset? Date { get; set; }
}