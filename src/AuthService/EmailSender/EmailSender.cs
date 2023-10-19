using System.Net;
using System.Net.Mail;

namespace AuthService.EmailSender;

public class EmailSender : IEmailSender
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _senderEmail;
    private readonly string _password;

    public EmailSender(IConfiguration configuration)
    {
        _smtpServer = configuration["EmailSender:SMTPServer"];
        _smtpPort = int.Parse(configuration["EmailSender:SMTPPort"]);
        _senderEmail = configuration["EmailSender:SenderEmail"];
        _password = configuration["EmailSender:Password"];
    }

    public async Task SendEmailAsync(string subject, string message, string toEmail)
    {
        var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
        {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            EnableSsl = true,
            Credentials = new NetworkCredential(_senderEmail, _password)
        };

        var mailMessage = new MailMessage(new MailAddress(_senderEmail, "Mushroomer App"), new MailAddress(toEmail, toEmail))
        {
            
            Subject = subject,
            Body = message
        };

        try
        {
            smtpClient.Send(mailMessage);
            //_logger.LogInformation(response.IsSuccessStatusCode ? $"Email to {toEmail} queued successfully!"
            //  : $"Failure Email to {toEmail}");
        }
        catch (Exception ex) 
        {
            throw new Exception("Error while sending email");
        }
    }
}
