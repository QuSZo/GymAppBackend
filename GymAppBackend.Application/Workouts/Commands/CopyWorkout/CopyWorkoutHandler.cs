using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.Exercises.Entities;
using GymAppBackend.Core.ExerciseSets.Entities;
using GymAppBackend.Core.Users.Exceptions;
using GymAppBackend.Core.Users.Repositories;
using GymAppBackend.Core.Workouts.Entities;
using GymAppBackend.Core.Workouts.Exceptions;
using GymAppBackend.Core.Workouts.Repositories;

namespace GymAppBackend.Application.Workouts.Commands.CopyWorkout;

internal sealed class CopyWorkoutHandler : ICommandHandler<CopyWorkoutCommand, CreateOrUpdateResponse>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public CopyWorkoutHandler(IWorkoutRepository workoutRepository, IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _workoutRepository = workoutRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(CopyWorkoutCommand command)
    {
        var user = await _userRepository.GetByIdAsync(_currentUserService.UserId);
        if (user == null)
        {
            throw new UserNotFoundException(_currentUserService.UserId);
        }

        var copyWorkoutTo = await _workoutRepository.GetByDateForUserAsync(command.DestinationDate, user.Id);
        if (copyWorkoutTo != null)
        {
            throw new WorkoutWithTheSameDateException(command.SourceDate);
        }

        var copyWorkoutFrom = await _workoutRepository.GetByDateForUserAsync(command.SourceDate, user.Id);
        if (copyWorkoutFrom == null)
        {
            throw new WorkoutByDateNotFoundException(command.SourceDate);
        }

        var workout = Workout.Create(command.Id, command.DestinationDate, user);
        var exercisesToCopyOrderedBy = copyWorkoutFrom.Exercises.OrderBy(exercise => exercise.ExerciseNumber);
        var exerciseNumber = 1;
        foreach (var exercise in exercisesToCopyOrderedBy)
        {
            var setNumber = 1;
            var newExercise = Exercise.Create(Guid.NewGuid(), exerciseNumber++, exercise.ExerciseType, workout);
            var exerciseSetsOrderedBy = exercise.ExerciseSets.OrderBy(exerciseSet => exerciseSet.SetNumber);
            foreach (var set in exerciseSetsOrderedBy)
            {
                newExercise.AddExerciseSet(ExerciseSet.Create(Guid.NewGuid(), setNumber++, set.Quantity, set.Reps, newExercise));
            }

            workout.AddExercise(newExercise);
        }

        await _workoutRepository.AddAsync(workout);

        return new CreateOrUpdateResponse(command.Id);
    }
}