using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.Exercises.Exceptions;
using GymAppBackend.Core.Exercises.Repositories;

namespace GymAppBackend.Application.Exercises.Commands.UpdateExerciseNumber;

internal sealed class UpdateExerciseNumberHandler : ICommandHandler<UpdateExerciseNumberCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateExerciseNumberHandler(IExerciseRepository exerciseRepository, ICurrentUserService currentUserService)
    {
        _exerciseRepository = exerciseRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(UpdateExerciseNumberCommand command)
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

        var exercises = await _exerciseRepository.GetAllByWorkoutIdAsync(exercise.Id);
        var exercisesList = exercises.ToList();
        var currentIndex = exercisesList.FindIndex(e => e.Id == exercise.Id);
        if (currentIndex == -1)
        {
            throw new ExerciseNotFoundException(command.Id);
        }

        int swapIndex = command.ChangeDirection switch
        {
            ChangeDirectionEnum.Up => currentIndex - 1,
            ChangeDirectionEnum.Down => currentIndex + 1,
            _ => throw new InvalidChangeDirectionValueException()
        };

        if (swapIndex < 0 || swapIndex >= exercisesList.Count)
        {
            throw new InvalidChangeDirectionValueException();
        }

        var exerciseToChange = exercisesList[swapIndex];
        int tempExerciseNumber = exerciseToChange.ExerciseNumber;
        exerciseToChange.UpdateExerciseNumber(exercise.ExerciseNumber);
        exercise.UpdateExerciseNumber(tempExerciseNumber);

        _exerciseRepository.Update(exercise);
        _exerciseRepository.Update(exerciseToChange);

        return new CreateOrUpdateResponse(command.Id);
    }
}