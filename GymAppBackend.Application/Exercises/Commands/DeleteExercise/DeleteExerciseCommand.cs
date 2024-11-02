using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Responses;

namespace GymAppBackend.Application.Exercises.Commands.DeleteExercise;

public sealed record DeleteExerciseCommand(Guid Id) : ICommand<CreateOrUpdateResponse>;