using System.Text.Json.Serialization;
using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;

namespace GymAppBackend.Application.ExerciseSets.Commands.UpdateExerciseSet;

public sealed record UpdateExerciseSetCommand(int Quantity, int Reps)
    : ICommand<CreateOrUpdateResponse>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}