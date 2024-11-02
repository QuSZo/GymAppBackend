using GymAppBackend.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Core.ExerciseSets.Exceptions;

public class ExerciseSetNotFoundException : CustomException
{
    public Guid ExerciseSetId { get; set; }

    public ExerciseSetNotFoundException(Guid exerciseSetId)
        : base($"Exercise set with id {exerciseSetId} does not exist.", StatusCodes.Status404NotFound)
    {
        ExerciseSetId = exerciseSetId;
    }
}