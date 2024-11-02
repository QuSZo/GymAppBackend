using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Core.Exercises.Exceptions;
using GymAppBackend.Core.Exercises.Repositories;

namespace GymAppBackend.Application.Exercises.Commands.DeleteExercise;

internal sealed class DeleteExerciseHandler : ICommandHandler<DeleteExerciseCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseRepository _exerciseRepository;

    public DeleteExerciseHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(DeleteExerciseCommand command)
    {
        var exercise = await _exerciseRepository.GetAsync(command.Id);
        if (exercise == null)
        {
            throw new ExerciseNotFoundException(command.Id);
        }

        await _exerciseRepository.DeleteAsync(exercise);

        return new CreateOrUpdateResponse(command.Id);
    }
}