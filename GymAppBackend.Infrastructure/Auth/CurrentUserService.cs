using System.Security.Claims;
using GymAppBackend.Application.Security;
using Microsoft.AspNetCore.Http;

namespace GymAppBackend.Infrastructure.Auth;

internal sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId => GetClaimAsGuid(ClaimTypes.NameIdentifier, _httpContextAccessor);

    private static Guid GetClaimAsGuid(string claimType, IHttpContextAccessor httpContextAccessor)
    {
        var claimAsString = httpContextAccessor?.HttpContext?.User.FindFirstValue(claimType);

        if (string.IsNullOrWhiteSpace(claimAsString))
            return Guid.Empty;

        var parseResultSuccessful = Guid.TryParse(claimAsString, out var claimId);

        if (!parseResultSuccessful || claimId == Guid.Empty)
            return Guid.Empty;

        return claimId;
    }
}