using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.Users.Entities;

namespace GymAppBackend.Core.PasswordResetTokens.Entities;

public class PasswordResetToken : Entity
{
    public string Token { get; private set; }
    public DateTime Expires { get; private set; }
    public User User { get; private set; }

    private PasswordResetToken()
    {
    }

    private PasswordResetToken(string token, DateTime expires, User user)
    {
        Token = token;
        Expires = expires;
        User = user;
    }

    public static PasswordResetToken Create(string token, DateTime expires, User user)
        => new(token, expires, user);
}