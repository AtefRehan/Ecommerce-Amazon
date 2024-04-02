using ECommerce.DTOS.SendEmail;
using ECommerce.Services.MailV;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost(Name = "SendEmail")]
        public void SendEmail(SendEmailDto emailDto)
        {
            _emailService.SendEmail(emailDto);
        }
    }
}
