using GymAppBackend.Application.Security.DTO;

namespace GymAppBackend.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}