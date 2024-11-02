using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;

namespace GymAppBackend.Application.ExerciseSets.Commands.DeleteExerciseSet;

public sealed record DeleteExerciseSetCommand(Guid Id) : ICommand<CreateOrUpdateResponse>;