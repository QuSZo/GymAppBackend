using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.Exercises.Exceptions;
using GymAppBackend.Core.Exercises.Repositories;
using GymAppBackend.Core.ExerciseSets.Entities;
using GymAppBackend.Core.ExerciseSets.Repositories;

namespace GymAppBackend.Application.ExerciseSets.Commands.CreateExerciseSet;

internal sealed class CreateExerciseSetHandler : ICommandHandler<CreateExerciseSetCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseSetRepository _exerciseSetRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ICurrentUserService _currentUserService;

    public CreateExerciseSetHandler(IExerciseSetRepository exerciseSetRepository, IExerciseRepository exerciseRepository, ICurrentUserService currentUserService)
    {
        _exerciseSetRepository = exerciseSetRepository;
        _exerciseRepository = exerciseRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(CreateExerciseSetCommand command)
    {
        var exercise = await _exerciseRepository.GetAsync(command.ExerciseId);
        if (exercise == null)
        {
            throw new ExerciseNotFoundException(command.ExerciseId);
        }

        if (exercise.Workout.User.Id != _currentUserService.UserId)
        {
            throw new ExerciseNotFoundException(command.ExerciseId);
        }

        var exerciseSetsOrderByNumber = exercise.ExerciseSets.OrderBy(exerciseSet => exerciseSet.SetNumber);
        var exerciseSetNumber = exerciseSetsOrderByNumber.LastOrDefault() != null ? exerciseSetsOrderByNumber.Last().SetNumber + 1 : 1;
        var exerciseSet = ExerciseSet.Create(command.Id, exerciseSetNumber, command.Quantity, command.Reps, exercise);
        await _exerciseSetRepository.AddAsync(exerciseSet);

        return new CreateOrUpdateResponse(command.Id);
    }
}