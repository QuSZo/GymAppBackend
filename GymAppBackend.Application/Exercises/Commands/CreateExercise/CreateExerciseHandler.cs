using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.Exercises.Entities;
using GymAppBackend.Core.Exercises.Repositories;
using GymAppBackend.Core.ExerciseTypes.Exceptions;
using GymAppBackend.Core.ExerciseTypes.Repositories;
using GymAppBackend.Core.Workouts.Exceptions;
using GymAppBackend.Core.Workouts.Repositories;

namespace GymAppBackend.Application.Exercises.Commands.CreateExercise;

internal sealed class CreateExerciseHandler : ICommandHandler<CreateExerciseCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseTypeRepository _exerciseTypeRepository;
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateExerciseHandler(
        IExerciseTypeRepository exerciseTypeRepository,
        IWorkoutRepository workoutRepository,
        IExerciseRepository exerciseRepository,
        ICurrentUserService currentUserService)
    {
        _exerciseTypeRepository = exerciseTypeRepository;
        _workoutRepository = workoutRepository;
        _exerciseRepository = exerciseRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(CreateExerciseCommand command)
    {
        var workout = await _workoutRepository.GetAsync(command.workoutId);
        if (workout == null)
        {
            throw new WorkoutNotFoundException(command.workoutId);
        }

        if (workout.User.Id != _currentUserService.UserId)
        {
            throw new WorkoutNotFoundException(command.workoutId);
        }

        var exerciseType = await _exerciseTypeRepository.GetAsync(command.exerciseTypeId);
        if (exerciseType == null)
        {
            throw new ExerciseTypeNotFoundException(command.exerciseTypeId);
        }

        var exerciseInWorkouts = await _exerciseRepository.GetAllByWorkoutIdAsync(command.workoutId);
        var nextExerciseNumber = exerciseInWorkouts.Count() + 1;
        var exercise = Exercise.Create(command.Id, nextExerciseNumber, exerciseType, workout);

        await _exerciseRepository.AddAsync(exercise);

        return new CreateOrUpdateResponse(command.Id);
    }
}