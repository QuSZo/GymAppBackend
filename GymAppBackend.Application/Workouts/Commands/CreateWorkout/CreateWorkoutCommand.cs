using GymAppBackend.Application.Abstractions;

namespace GymAppBackend.Application.Workouts.Commands.CreateWorkout;

public sealed record CreateWorkoutCommand() : ICommand
{
    internal Guid Id { get; } = Guid.NewGuid();
}