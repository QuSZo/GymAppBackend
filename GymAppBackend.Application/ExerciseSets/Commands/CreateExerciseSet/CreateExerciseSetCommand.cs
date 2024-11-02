using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;

namespace GymAppBackend.Application.ExerciseSets.Commands.CreateExerciseSet;

public sealed record CreateExerciseSetCommand(Guid ExerciseId, int Quantity, int Reps) : ICommand<CreateOrUpdateResponse>
{
    internal Guid Id { get; } = Guid.NewGuid();
}