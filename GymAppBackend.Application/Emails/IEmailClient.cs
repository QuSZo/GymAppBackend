namespace GymAppBackend.Application.Emails;

public interface IEmailClient
{
    public Task SendEmailAsync(string email, string subject, string message);
}