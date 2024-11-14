using GymAppBackend.Core.Abstractions;
using GymAppBackend.Core.ValueObjects.Email;
using GymAppBackend.Core.ValueObjects.Password;
using GymAppBackend.Core.ValueObjects.Role;

namespace GymAppBackend.Core.Users.Entities;

public class User : Entity
{
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User()
    {
    }

    private User(Guid id, Email email, Password password, Role role, DateTime createdAt)
    {
        Id = id;
        Email = email;
        Password = password;
        Role = role;
        CreatedAt = createdAt;
    }

    public static User Create(Guid id, Email email, Password password, Role role, DateTime createdAt)
    => new(id, email, password, role, createdAt);

    public void ResetPassword(Password newPassword)
    {
        Password = newPassword;
    }
}