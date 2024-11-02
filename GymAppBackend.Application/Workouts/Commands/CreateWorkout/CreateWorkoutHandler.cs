using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.Exercises.Entities;
using GymAppBackend.Core.ExerciseTypes.Exceptions;
using GymAppBackend.Core.ExerciseTypes.Repositories;
using GymAppBackend.Core.Workouts.Entities;
using GymAppBackend.Core.Workouts.Exceptions;
using GymAppBackend.Core.Workouts.Repositories;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Application.Workouts.Commands.CreateWorkout;

internal sealed class CreateWorkoutHandler : ICommandHandler<CreateWorkoutCommand, CreateOrUpdateResponse>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IExerciseTypeRepository _exerciseTypeRepository;
    private readonly IClock _clock;

    public CreateWorkoutHandler(IWorkoutRepository workoutRepository, IClock clock, IExerciseTypeRepository exerciseTypeRepository)
    {
        _workoutRepository = workoutRepository;
        _clock = clock;
        _exerciseTypeRepository = exerciseTypeRepository;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(CreateWorkoutCommand command)
    {
        var isSameWorkoutDate = await _workoutRepository.GetByDateAsync(command.Date);
        if (isSameWorkoutDate != null)
        {
            throw new WorkoutWithTheSameDateException(command.Date);
        }

        var workout = Workout.Create(command.Id, command.Date);

        var exerciseType = await _exerciseTypeRepository.GetAsync(command.ExerciseTypeId);
        if (exerciseType == null)
        {
            throw new ExerciseTypeNotFoundException(command.ExerciseTypeId);
        }

        var exercise = Exercise.Create(Guid.NewGuid(), 1, exerciseType, workout);
        workout.AddExercise(exercise);

        await _workoutRepository.AddAsync(workout);

        return new CreateOrUpdateResponse(command.Id);
    }
}