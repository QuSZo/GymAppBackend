using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Users.Queries.DTO;
using GymAppBackend.Application.Users.Queries.GetUser;

namespace GymAppBackend.Infrastructure.DAL.Users.Queries.GetUser;

internal sealed class GetUserHandler : IQueryHandler<GetUserQuery, UserDto>
{
    public Task<UserDto> HandleAsync(GetUserQuery query)
    {
        throw new NotImplementedException();
    }
}