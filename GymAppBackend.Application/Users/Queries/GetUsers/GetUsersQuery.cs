using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Users.Queries.DTO;

namespace GymAppBackend.Application.Users.Queries.GetUsers;

public class GetUsersQuery : IQuery<IEnumerable<UserDto>>
{
}