using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;

namespace GymAppBackend.Application.Workouts.Commands.CopyWorkout;

public record CopyWorkoutCommand(DateTime DestinationDate, DateTime SourceDate) : ICommand<CreateOrUpdateResponse>
{
    internal Guid Id { get; init; } = Guid.NewGuid();
}