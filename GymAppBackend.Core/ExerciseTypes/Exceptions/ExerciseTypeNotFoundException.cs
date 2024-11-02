using GymAppBackend.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Core.ExerciseTypes.Exceptions;

public class ExerciseTypeNotFoundException : CustomException
{
    public Guid ExerciseTypeId { get; }

    public ExerciseTypeNotFoundException(Guid exerciseTypeId)
        : base($"Exercise type with id {exerciseTypeId} does not exist.", StatusCodes.Status404NotFound)
    {
        ExerciseTypeId = exerciseTypeId;
    }
}