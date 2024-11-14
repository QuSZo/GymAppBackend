using GymAppBackend.Application.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymAppBackend.Infrastructure.Emails;

internal static class Extensions
{
    private const string SectionName = "sendgrid";

    public static IServiceCollection AddSendGrid(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(configuration.GetRequiredSection(SectionName));
        services.AddScoped<IEmailClient, EmailClient>();

        return services;
    }
}