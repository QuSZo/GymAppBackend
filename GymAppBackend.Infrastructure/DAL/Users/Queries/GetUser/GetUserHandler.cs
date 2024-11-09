using GymAppBackend.Application.Abstractions;
using GymAppBackend.Application.Users.Queries.DTO;
using GymAppBackend.Application.Users.Queries.GetUser;
using GymAppBackend.Core.Users.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GymAppBackend.Infrastructure.DAL.Users.Queries.GetUser;

internal sealed class GetUserHandler : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly GymAppDbContext _dbContext;

    public GetUserHandler(GymAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto> HandleAsync(GetUserQuery query)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == query.Id);

        if (user == null)
        {
            throw new UserNotFoundException(query.Id);
        }

        return user.AsDto();
    }
}