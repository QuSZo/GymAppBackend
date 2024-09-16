using GymAppBackend.Application.Abstractions;

namespace GymAppBackend.Application.Commands.CreateWorkout;

public record CreateWorkoutCommand(string WorkoutName) : ICommand;