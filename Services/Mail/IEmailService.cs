using ECommerce.DTOS.SendEmail;

namespace ECommerce.Services.MailV;

public interface IEmailService
{
    public void SendEmail(SendEmailDto emailDto);

}