using GymAppBackend.Application.Abstractions;
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

    public GetWorkoutHandler(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WorkoutDetailsDto> HandleAsync(GetWorkoutQuery query)
    {
        var workout = await _dbContext.Workouts
            .AsNoTracking()
            .Include(workout => workout.Exercises)
                .ThenInclude(exercises => exercises.ExerciseType)
            .Include(workout => workout.Exercises)
                .ThenInclude(exercises => exercises.ExerciseSets)
            .SingleOrDefaultAsync(workout => workout.Id == query.Id);

        if (workout == null)
        {
            throw new WorkoutNotFoundException(query.Id);
        }

        return workout.AsDetailsDto();
    }
}