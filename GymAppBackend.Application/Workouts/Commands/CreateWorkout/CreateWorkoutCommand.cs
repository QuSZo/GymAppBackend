using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;
using GymAppBackend.Core.Exercises.Entities;
using GymAppBackend.Core.ValueObjects;

namespace GymAppBackend.Application.Workouts.Commands.CreateWorkout;

public sealed record CreateWorkoutCommand(DateTime Date, Guid ExerciseTypeId) : ICommand<CreateOrUpdateResponse>
{
    internal Guid Id { get; } = Guid.NewGuid();
}