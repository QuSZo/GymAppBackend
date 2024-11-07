using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Users.Queries.DTO;
using GymAppBackend.Application.Users.Queries.GetUsers;

namespace GymAppBackend.Infrastructure.DAL.Users.Queries.GetUsers;

internal sealed class GetUsersHandler : IQueryHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    public Task<IEnumerable<UserDto>> HandleAsync(GetUsersQuery query)
    {
        throw new NotImplementedException();
    }
}