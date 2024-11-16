using System.Text.Json.Serialization;
using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;

namespace GymAppBackend.Application.Exercises.Commands.UpdateExerciseNumber;

public sealed record UpdateExerciseNumberCommand(ChangeDirectionEnum ChangeDirection) : ICommand<CreateOrUpdateResponse>
{
    [JsonIgnore]
    public Guid Id { get; init; }
}