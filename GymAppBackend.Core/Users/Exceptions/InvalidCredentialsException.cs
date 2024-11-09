using GymAppBackend.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Core.Users.Exceptions;

public class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Ivalid credentials.", StatusCodes.Status400BadRequest)
    {
    }
}