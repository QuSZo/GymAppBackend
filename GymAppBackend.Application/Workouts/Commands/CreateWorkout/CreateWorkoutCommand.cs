using GymAppBackend.Application.Abstractions;

namespace GymAppBackend.Application.Workouts.Commands.CreateWorkout;

public record CreateWorkoutCommand(string WorkoutName) : ICommand;