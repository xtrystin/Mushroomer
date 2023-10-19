namespace AuthService.EmailSender;

public interface IEmailSender
{
    Task SendEmailAsync(string subject, string message, string toEmail);
}
