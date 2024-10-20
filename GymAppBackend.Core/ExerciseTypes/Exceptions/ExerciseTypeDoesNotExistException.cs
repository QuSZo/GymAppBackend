using GymAppBackend.Core.Exceptions;

namespace GymAppBackend.Core.ExerciseTypes.Exceptions;

public class ExerciseTypeDoesNotExistException : CustomException
{
    public Guid ExerciseTypeId { get; }

    public ExerciseTypeDoesNotExistException(Guid exerciseTypeId, int statusCode)
        : base($"Exercise type with {exerciseTypeId} does not exist.", statusCode)
    {
        ExerciseTypeId = exerciseTypeId;
    }
}