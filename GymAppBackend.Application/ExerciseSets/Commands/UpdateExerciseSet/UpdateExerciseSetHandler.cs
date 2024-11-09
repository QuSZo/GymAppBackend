using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.ExerciseSets.Exceptions;
using GymAppBackend.Core.ExerciseSets.Repositories;

namespace GymAppBackend.Application.ExerciseSets.Commands.UpdateExerciseSet;

internal sealed class UpdateExerciseSetHandler : ICommandHandler<UpdateExerciseSetCommand, CreateOrUpdateResponse>
{
    private readonly IExerciseSetRepository _exerciseSetRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateExerciseSetHandler(IExerciseSetRepository exerciseSetRepository, ICurrentUserService currentUserService)
    {
        _exerciseSetRepository = exerciseSetRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(UpdateExerciseSetCommand command)
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

        exerciseSet.Update(command.Quantity, command.Reps);
        await _exerciseSetRepository.UpdateAsync(exerciseSet);

        return new CreateOrUpdateResponse(command.Id);
    }
}