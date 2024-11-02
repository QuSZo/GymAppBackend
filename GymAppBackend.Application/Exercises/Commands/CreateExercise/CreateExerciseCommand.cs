using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;

namespace GymAppBackend.Application.Exercises.Commands.CreateExercise;

public sealed record CreateExerciseCommand(Guid exerciseTypeId, Guid workoutId) : ICommand<CreateOrUpdateResponse>
{
    internal Guid Id { get; } = Guid.NewGuid();
}