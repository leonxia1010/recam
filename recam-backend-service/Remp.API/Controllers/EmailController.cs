using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Remp.Application.DTOs.Email;
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
        public async Task<IActionResult> SendEmailAsync([FromBody] EmailRequestDTO emailRequestDTO)
        {
            var receiverEmail = emailRequestDTO.ReceiverEmail;
            var subject = emailRequestDTO.Subject;
            var body = emailRequestDTO.Body;
            await _emailService.SendEmailAsync(receiverEmail, subject, body);
            return Ok("Send Email Successfully");
        }
    }
}
