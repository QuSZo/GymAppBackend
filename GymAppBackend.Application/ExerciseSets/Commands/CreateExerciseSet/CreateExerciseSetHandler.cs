using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Core.Exercises.Exceptions;
using GymAppBackend.Core.Exercises.Repositories;
using GymAppBackend.Core.ExerciseSets.Entities;
using GymAppBackend.Core.ExerciseSets.Repositories;

namespace GymAppBackend.Application.ExerciseSets.Commands.CreateExerciseSet;

internal sealed class CreateExerciseSetHandler : ICommandHandler<CreateExerciseSetCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseSetRepository _exerciseSetRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public CreateExerciseSetHandler(IExerciseSetRepository exerciseSetRepository, IExerciseRepository exerciseRepository)
    {
        _exerciseSetRepository = exerciseSetRepository;
        _exerciseRepository = exerciseRepository;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(CreateExerciseSetCommand command)
    {
        var exercise = await _exerciseRepository.GetAsync(command.ExerciseId);
        if (exercise == null)
        {
            throw new ExerciseNotFoundException(command.ExerciseId);
        }

        var exerciseSetNumber = exercise.ExerciseSets.Count() + 1;
        var exerciseSet = ExerciseSet.Create(command.Id, exerciseSetNumber, command.Quantity, command.Reps, exercise);
        await _exerciseSetRepository.AddAsync(exerciseSet);

        return new CreateOrUpdateResponse(command.Id);
    }
}