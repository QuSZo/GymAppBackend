using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.Exercises.Exceptions;
using GymAppBackend.Core.Exercises.Repositories;
using GymAppBackend.Core.Workouts.Repositories;

namespace GymAppBackend.Application.Exercises.Commands.DeleteExercise;

internal sealed class DeleteExerciseHandler : ICommandHandler<DeleteExerciseCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly IWorkoutRepository _workoutRepository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteExerciseHandler(IExerciseRepository exerciseRepository, IWorkoutRepository workoutRepository, ICurrentUserService currentUserService)
    {
        _exerciseRepository = exerciseRepository;
        _workoutRepository = workoutRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(DeleteExerciseCommand command)
    {
        var exercise = await _exerciseRepository.GetAsync(command.Id);
        if (exercise == null)
        {
            throw new ExerciseNotFoundException(command.Id);
        }

        if (exercise.Workout.User.Id != _currentUserService.UserId)
        {
            throw new ExerciseNotFoundException(command.Id);
        }

        if (exercise.Workout.Exercises.Count() == 1)
        {
            await _workoutRepository.DeleteAsync(exercise.Workout);
        }
        else
        {
            await _exerciseRepository.DeleteAsync(exercise);
        }

        return new CreateOrUpdateResponse(command.Id);
    }
}