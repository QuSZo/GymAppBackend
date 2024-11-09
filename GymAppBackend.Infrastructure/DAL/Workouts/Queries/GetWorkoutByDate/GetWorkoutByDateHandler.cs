using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Security;
using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Application.Workouts.Queries.GetWorkoutByDate;
using GymAppBackend.Core.ValueObjects;
using GymAppBackend.Core.ValueObjects.Date;
using GymAppBackend.Core.Workouts.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.Workouts.Queries.GetWorkoutByDate;

internal sealed class GetWorkoutByDateHandler : IQueryHandler<GetWorkoutByDateQuery, WorkoutDetailsDto>
{
    private readonly GymAppDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public GetWorkoutByDateHandler(GymAppDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }

    public async Task<WorkoutDetailsDto> HandleAsync(GetWorkoutByDateQuery query)
    {
        var dayStarted = new Date(new DateTime(query.Date.Year, query.Date.Month, query.Date.Day, 0, 0, 0));
        var dayEnded = new Date(dayStarted).AddDays(1);

        var workout = await _dbContext.Workouts
            .AsNoTracking()
            .Include(workout => workout.User)
            .Include(workout => workout.Exercises)
            .ThenInclude(exercises => exercises.ExerciseType)
            .Include(workout => workout.Exercises)
            .ThenInclude(exercises => exercises.ExerciseSets)
            .SingleOrDefaultAsync(workout => workout.Date >= dayStarted && workout.Date < dayEnded && workout.User.Id == _currentUserService.UserId);

        if (workout == null)
        {
            throw new WorkoutByDateNotFoundException(query.Date);
        }

        return workout.AsDetailsDto();
    }
}