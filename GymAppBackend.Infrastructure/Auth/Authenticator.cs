using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GymAppBackend.Application.Security;
using GymAppBackend.Application.Security.DTO;
using GymAppBackend.Core.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GymAppBackend.Infrastructure.Auth;

internal sealed class Authenticator : IAuthenticator
{
    private readonly IClock _clock;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly TimeSpan _tokenLifetime;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

    public Authenticator(IOptions<AuthOptions> authOptions, IClock clock)
    {
        _clock = clock;
        _issuer = authOptions.Value.Issuer;
        _audience = authOptions.Value.Audience;
        _tokenLifetime = authOptions.Value.TokenLifetime ?? TimeSpan.FromHours(12);
        _signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Value.SigningKey)),
            SecurityAlgorithms.HmacSha256);
    }

    public JwtDto CreateToken(Guid userId, string role)
    {
        var now = _clock.Current();
        var tokenLifeTime = now.Add(_tokenLifetime);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new Claim(ClaimTypes.Role, role),
        };

        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, tokenLifeTime, _signingCredentials);
        var accessToken = _jwtSecurityTokenHandler.WriteToken(jwt);

        return new JwtDto
        {
            AccessToken = accessToken,
        };
    }
}