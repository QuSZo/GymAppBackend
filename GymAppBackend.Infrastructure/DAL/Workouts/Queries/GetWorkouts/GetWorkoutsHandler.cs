using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Security;
using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Application.Workouts.Queries.GetWorkouts;
using GymAppBackend.Core.Workouts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.Workouts.Queries.GetWorkouts;

internal sealed class GetWorkoutsHandler : IQueryHandler<GetWorkoutsQuery, IEnumerable<WorkoutsDto>>
{
    private readonly GymAppDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public GetWorkoutsHandler(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<WorkoutsDto>> HandleAsync(GetWorkoutsQuery query)
    {
        var workouts = await _dbContext.Workouts
            .AsNoTracking()
            .Include(workout => workout.User)
            .Where(workout => workout.User.Id == _currentUserService.UserId)
            .Select(workout => workout.AsDto())
            .ToListAsync();

        return workouts;
    }
}