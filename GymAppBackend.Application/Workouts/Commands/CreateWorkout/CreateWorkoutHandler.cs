using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Application.Security;
using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.Exercises.Entities;
using GymAppBackend.Core.ExerciseTypes.Exceptions;
using GymAppBackend.Core.ExerciseTypes.Repositories;
using GymAppBackend.Core.Users.Exceptions;
using GymAppBackend.Core.Users.Repositories;
using GymAppBackend.Core.Workouts.Entities;
using GymAppBackend.Core.Workouts.Exceptions;
using GymAppBackend.Core.Workouts.Repositories;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Application.Workouts.Commands.CreateWorkout;

internal sealed class CreateWorkoutHandler : ICommandHandler<CreateWorkoutCommand, CreateOrUpdateResponse>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IExerciseTypeRepository _exerciseTypeRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;

    public CreateWorkoutHandler(
        IWorkoutRepository workoutRepository,
        IExerciseTypeRepository exerciseTypeRepository,
        ICurrentUserService currentUserService,
        IUserRepository userRepository)
    {
        _workoutRepository = workoutRepository;
        _exerciseTypeRepository = exerciseTypeRepository;
        _currentUserService = currentUserService;
        _userRepository = userRepository;
    }

    public async Task<CreateOrUpdateResponse> HandleAsync(CreateWorkoutCommand command)
    {
        var user = await _userRepository.GetByIdAsync(_currentUserService.UserId);
        if (user == null)
        {
            throw new UserNotFoundException(_currentUserService.UserId);
        }

        var isSameWorkoutDate = await _workoutRepository.GetByDateForUserAsync(command.Date, user.Id);
        if (isSameWorkoutDate != null)
        {
            throw new WorkoutWithTheSameDateException(command.Date);
        }

        var workout = Workout.Create(command.Id, command.Date, user);

        var exerciseType = await _exerciseTypeRepository.GetAsync(command.ExerciseTypeId);
        if (exerciseType == null)
        {
            throw new ExerciseTypeNotFoundException(command.ExerciseTypeId);
        }

        var exercise = Exercise.Create(Guid.NewGuid(), 1, exerciseType, workout);
        workout.AddExercise(exercise);

        await _workoutRepository.AddAsync(workout);

        return new CreateOrUpdateResponse(command.Id);
    }
}