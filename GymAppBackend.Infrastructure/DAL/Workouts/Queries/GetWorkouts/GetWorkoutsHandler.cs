using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Application.Workouts.Queries.GetWorkouts;
using GymAppBackend.Core.Workouts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.Workouts.Queries.GetWorkouts;

internal sealed class GetWorkoutsHandler : IQueryHandler<GetWorkoutsQuery, IEnumerable<WorkoutsDto>>
{
    private readonly GymAppDbContext _dbContext;

    public GetWorkoutsHandler(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WorkoutsDto>> HandleAsync(GetWorkoutsQuery query)
    {
        var workouts = await _dbContext.Workouts
            .AsNoTracking()
            .Select(x => x.AsDto())
            .ToListAsync();

        return workouts;
    }
}