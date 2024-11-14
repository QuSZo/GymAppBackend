using GymAppBackend.Application.Emails;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace GymAppBackend.Infrastructure.Emails;

internal sealed class EmailClient : IEmailClient
{
    private readonly string _apiKey;
    private readonly string _fromEmail;
    private readonly string _fromName;

    public EmailClient(IOptions<EmailOptions> options)
    {
        _apiKey = options.Value.ApiKey;
        _fromEmail = options.Value.FromEmail;
        _fromName = options.Value.FromName;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress(_fromEmail, _fromName);
        var to = new EmailAddress(email);

        var plainTextContent = message;
        var htmlContent = $"<p>{message}</p>";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        await client.SendEmailAsync(msg).ConfigureAwait(false);
    }
}