using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Core.ValueObjects;

namespace GymAppBackend.Application.Workouts.Queries.GetWorkouts;

public class GetWorkoutsQuery : IQuery<IEnumerable<WorkoutsDto>>
{
}