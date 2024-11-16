using GymAppBackend.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Application.Exercises.Commands.UpdateExerciseNumber;

public class InvalidChangeDirectionValueException : CustomException
{
    public InvalidChangeDirectionValueException() : base("Invalid change direction value", StatusCodes.Status400BadRequest)
    {
    }
}