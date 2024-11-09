using System.Text;
using GymAppBackend.Application.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GymAppBackend.Infrastructure.Auth;

internal static class Extensions
{
    private const string SectionName = "auth";

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetRequiredSection(SectionName));
        var authOptions = configuration.GetOptions<AuthOptions>(SectionName);

        services
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Audience = authOptions.Audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authOptions.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SigningKey)),
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("is-admin", policy => policy.RequireRole("admin"));
        });

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}