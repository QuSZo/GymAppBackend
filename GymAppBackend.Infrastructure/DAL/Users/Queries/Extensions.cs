using GymAppBackend.Application.Users.Queries.DTO;
using GymAppBackend.Core.Users.Entities;

namespace GymAppBackend.Infrastructure.DAL.Users.Queries;

public static class Extensions
{
    public static UserDto AsDto(this User entity)
        => new()
        {
            Id = entity.Id,
        };
}