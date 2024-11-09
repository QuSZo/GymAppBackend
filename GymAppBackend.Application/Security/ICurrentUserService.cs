namespace GymAppBackend.Application.Security;

public interface ICurrentUserService
{
    Guid UserId { get; }
}