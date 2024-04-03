using ECommerce.DTOS.SendEmail;
using ECommerce.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace ECommerce.Services.MailV;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfig;

    public EmailService(IOptions<EmailConfiguration> emailConfig)
    {
        _emailConfig = emailConfig.Value;
    }


    public void SendEmail(SendEmailDto emailDto)
    {
        var email = new MimeMessage
        {
            Subject = emailDto.Subject,
            To = { MailboxAddress.Parse(emailDto.To) },
            Body = new TextPart(TextFormat.Html)
            {
                Text = emailDto.Html
            },
            From = { new MailboxAddress("Amazon", _emailConfig.From) }
        };
        using (var smtp = new SmtpClient())
        {
            smtp.Connect(_emailConfig.Host, _emailConfig.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailConfig.From, _emailConfig.Password);

            var response = smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}