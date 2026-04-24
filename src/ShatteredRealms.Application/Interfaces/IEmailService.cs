namespace ShatteredRealms.Application.Interfaces;

public interface IEmailService
{
    Task SendEmailConfirmationAsync(
        string toEmail,
        string toName,
        string confirmationLink,
        CancellationToken cancellationToken = default);
}
