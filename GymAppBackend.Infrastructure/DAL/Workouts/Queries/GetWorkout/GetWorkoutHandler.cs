using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Security;
using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Application.Workouts.Queries.GetWorkout;
using GymAppBackend.Core.Workouts.Exceptions;
using GymAppBackend.Core.Workouts.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.Workouts.Queries.GetWorkout;

internal sealed class GetWorkoutHandler : IQueryHandler<GetWorkoutQuery, WorkoutDetailsDto>
{
    private readonly GymAppDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public GetWorkoutHandler(GymAppDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }

    public async Task<WorkoutDetailsDto> HandleAsync(GetWorkoutQuery query)
    {
        var workout = await _dbContext.Workouts
            .AsNoTracking()
            .Include(workout => workout.User)
            .Include(workout => workout.Exercises)
                .ThenInclude(exercises => exercises.ExerciseType)
            .Include(workout => workout.Exercises)
                .ThenInclude(exercises => exercises.ExerciseSets)
            .SingleOrDefaultAsync(workout => workout.Id == query.Id && workout.User.Id == _currentUserService.UserId);

        if (workout == null)
        {
            throw new WorkoutNotFoundException(query.Id);
        }

        return workout.AsDetailsDto();
    }
}