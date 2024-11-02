using GymAppBackend.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Core.Exercises.Exceptions;

public sealed class ExerciseNotFoundException : CustomException
{
    public Guid ExerciseId { get; }

    public ExerciseNotFoundException(Guid exerciseId) : base($"Exercise with id {exerciseId} does not exist.", StatusCodes.Status404NotFound)
    {
        ExerciseId = exerciseId;
    }
}