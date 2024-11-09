using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.ExerciseSets.Exceptions;
using GymAppBackend.Core.ExerciseSets.Repositories;

namespace GymAppBackend.Application.ExerciseSets.Commands.DeleteExerciseSet;

internal sealed class DeleteExerciseSetHandler : ICommandHandler<DeleteExerciseSetCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseSetRepository _exerciseSetRepository;
    private readonly ICurrentUserService _currentUserService;

    public DeleteExerciseSetHandler(IExerciseSetRepository exerciseSetRepository, ICurrentUserService currentUserService)
    {
        _exerciseSetRepository = exerciseSetRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(DeleteExerciseSetCommand command)
    {
        var exerciseSet = await _exerciseSetRepository.GetAsync(command.Id);
        if (exerciseSet == null)
        {
            throw new ExerciseSetNotFoundException(command.Id);
        }

        if (exerciseSet.Exercise.Workout.User.Id != _currentUserService.UserId)
        {
            throw new ExerciseSetNotFoundException(command.Id);
        }

        await _exerciseSetRepository.DeleteAsync(exerciseSet);

        return new CreateOrUpdateResponse(command.Id);
    }
}