using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShatteredRealms.Application.Interfaces;

namespace ShatteredRealms.Infrastructure.Services;

public sealed class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailConfirmationAsync(
        string toEmail,
        string toName,
        string confirmationLink,
        CancellationToken cancellationToken = default)
    {
        var smtpHost = _configuration["Email:SmtpHost"];

        if (string.IsNullOrWhiteSpace(smtpHost))
        {
            _logger.LogInformation(
                "EMAIL CONFIRMATION (no SMTP configured — dev mode)\nTo: {Email}\nConfirmation link: {Link}",
                toEmail, confirmationLink);
            return;
        }

        var fromAddress = _configuration["Email:FromAddress"] ?? "noreply@shatteredrealms.com";
        var fromName    = _configuration["Email:FromName"]    ?? "ShatteredRealms";
        var port        = int.TryParse(_configuration["Email:SmtpPort"], out var p) ? p : 587;
        var username    = _configuration["Email:Username"];
        var password    = _configuration["Email:Password"];
        var enableSsl   = !string.Equals(_configuration["Email:EnableSsl"], "false", StringComparison.OrdinalIgnoreCase);

        using var message = new MailMessage
        {
            From       = new MailAddress(fromAddress, fromName),
            Subject    = "Confirm your ShatteredRealms account",
            Body       = BuildConfirmationEmailHtml(toName, confirmationLink),
            IsBodyHtml = true,
        };
        message.To.Add(new MailAddress(toEmail, toName));

        using var smtp = new SmtpClient(smtpHost, port)
        {
            EnableSsl   = enableSsl,
            Credentials = !string.IsNullOrEmpty(username)
                ? new NetworkCredential(username, password)
                : null,
        };

        await smtp.SendMailAsync(message, cancellationToken);
        _logger.LogInformation("Sent email confirmation to {Email}", toEmail);
    }

    private static string BuildConfirmationEmailHtml(string name, string link) => $"""
        <!DOCTYPE html>
        <html lang="en">
        <body style="margin:0;padding:0;background:#07090f;font-family:Georgia,serif;">
          <table width="100%" cellpadding="0" cellspacing="0" style="background:#07090f;padding:3rem 1rem;">
            <tr><td align="center">
              <table width="520" cellpadding="0" cellspacing="0"
                     style="background:#0d1219;border:1px solid #1c2a3e;border-radius:12px;padding:2.5rem;">
                <tr>
                  <td>
                    <p style="font-size:1.4rem;font-weight:bold;color:#c9a84c;margin:0 0 0.25rem 0;letter-spacing:0.04em;">
                      ShatteredRealms
                    </p>
                    <p style="font-size:0.8rem;color:#5e7189;letter-spacing:0.12em;text-transform:uppercase;margin:0 0 2rem 0;">
                      Email Confirmation
                    </p>
                    <p style="color:#d8cdb8;font-size:1rem;line-height:1.6;margin:0 0 1rem 0;">
                      Welcome, {name}. Your account has been created.
                    </p>
                    <p style="color:#d8cdb8;font-size:1rem;line-height:1.6;margin:0 0 2rem 0;">
                      Click the button below to confirm your email address and activate your account:
                    </p>
                    <a href="{link}"
                       style="display:inline-block;background:#c9a84c;color:#0a0c10;text-decoration:none;
                              padding:0.75rem 2rem;border-radius:8px;font-weight:bold;font-size:0.875rem;
                              letter-spacing:0.05em;">
                      Confirm Email Address
                    </a>
                    <p style="margin-top:2rem;color:#5e7189;font-size:0.8rem;line-height:1.5;">
                      This link expires in 24 hours. If you did not create an account, you can safely ignore this email.
                    </p>
                  </td>
                </tr>
              </table>
            </td></tr>
          </table>
        </body>
        </html>
        """;
}
