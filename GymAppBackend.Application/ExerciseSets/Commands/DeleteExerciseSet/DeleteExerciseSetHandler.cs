using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Core.ExerciseSets.Exceptions;
using GymAppBackend.Core.ExerciseSets.Repositories;

namespace GymAppBackend.Application.ExerciseSets.Commands.DeleteExerciseSet;

internal sealed class DeleteExerciseSetHandler : ICommandHandler<DeleteExerciseSetCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseSetRepository _exerciseSetRepository;

    public DeleteExerciseSetHandler(IExerciseSetRepository exerciseSetRepository)
    {
        _exerciseSetRepository = exerciseSetRepository;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(DeleteExerciseSetCommand command)
    {
        var exerciseSet = await _exerciseSetRepository.GetAsync(command.Id);
        if (exerciseSet == null)
        {
            throw new ExerciseSetNotFoundException(command.Id);
        }

        await _exerciseSetRepository.DeleteAsync(exerciseSet);

        return new CreateOrUpdateResponse(command.Id);
    }
}