using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Users.Queries.DTO;

namespace GymAppBackend.Application.Users.Queries.GetUser;

public class GetUserQuery : IQuery<UserDto>
{
    public Guid Id { get; set; }
}