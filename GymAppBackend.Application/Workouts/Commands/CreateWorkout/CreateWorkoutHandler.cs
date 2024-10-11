using GymAppBackend.Application.Abstractions;
using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ValueObjects;
using GymAppBackend.Core.Workouts.Entities;
using GymAppBackend.Core.Workouts.Repositories;

namespace GymAppBackend.Application.Workouts.Commands.CreateWorkout;

public class CreateWorkoutHandler : ICommandHandler<CreateWorkoutCommand>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IClock _clock;

    public CreateWorkoutHandler(IWorkoutRepository workoutRepository, IClock clock)
    {
        _workoutRepository = workoutRepository;
        _clock = clock;
    }

    public async Task HandleAsync(CreateWorkoutCommand command)
    {
        var workout = Workout.Create(command.Id, new Date(_clock.Current()));

        await _workoutRepository.AddAsync(workout);
    }
}