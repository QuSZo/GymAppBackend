using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Workouts.Queries.DTO;
using GymAppBackend.Application.Workouts.Queries.GetWorkout;
using GymAppBackend.Core.Workouts.Exceptions;
using GymAppBackend.Core.Workouts.Repositories;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Infrastructure.DAL.Workouts.Queries.GetWorkout;

public sealed class GetWorkoutHandler : IQueryHandler<GetWorkoutQuery, WorkoutDetailsDto>
{
    private readonly IWorkoutRepository _workoutRepository;

    public GetWorkoutHandler(IWorkoutRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }

    public async Task<WorkoutDetailsDto> HandleAsync(GetWorkoutQuery query)
    {
        var workout = await _workoutRepository.GetByDateAsync(query.Date);
        if (workout == null)
        {
            throw new WorkoutNotFoundException(query.Date, StatusCodes.Status404NotFound);
        }

        return workout.AsDetailsDto();
    }
}