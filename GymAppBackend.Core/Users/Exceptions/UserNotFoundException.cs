using GymAppBackend.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Core.Users.Exceptions;

public class UserNotFoundException : CustomException
{
    public Guid UserId { get; }

    public UserNotFoundException(Guid userId) : base($"User with id {userId} does not exist.", StatusCodes.Status404NotFound)
    {
        UserId = userId;
    }
}