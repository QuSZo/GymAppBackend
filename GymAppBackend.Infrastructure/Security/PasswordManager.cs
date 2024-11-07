using GymAppBackend.Application.Security;
using GymAppBackend.Core.Users.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymAppBackend.Infrastructure.Security;

internal sealed class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordManager(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Secure(string password)
    {
        return _passwordHasher.HashPassword(default, password);
    }

    public bool Validate(string password, string securedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(default, securedPassword, password) == PasswordVerificationResult.Success;
    }
}