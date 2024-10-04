using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Application.Workouts.Queries.GetWorkouts;
using GymAppBackend.Core.Repositories;
using GymAppBackend.Infrastructure.DAL.Repositories;

namespace GymAppBackend.Infrastructure.DAL.Workouts.Queries.GetWorkouts;

public sealed class GetWorkoutsHandler : IQueryHandler<GetWorkoutsQuery, IEnumerable<WorkoutsDto>>
{
    private readonly IWorkoutRepository _repository;

    public GetWorkoutsHandler(IWorkoutRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WorkoutsDto>> HandleAsync(GetWorkoutsQuery query)
    {
        var workouts = await _repository.GetWorkoutsAsync();

        return workouts.Select(x => x.AsDto());
    }
}