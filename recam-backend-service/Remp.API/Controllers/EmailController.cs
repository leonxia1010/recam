using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Remp.Application.Interfaces;
using Remp.Model.Settings;

namespace Remp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendEmailAsync(string to, string subject, string body)
        {
            await _emailService.SendEmailAsync(to, subject, body);
            return Ok("Send Email Successfully");
        }
    }
}
