using Mailer.Model;
using Mailer.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

[Route("mailerapi/[action]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly SmtpSettings _smtpSettings;

    public UsersController(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    [HttpPost(Name = "sendEmail")]
    public async Task<IActionResult> SendEmail([FromBody] User user)
    {
        if (user == null)
        {
            return BadRequest("User data is null");
        }

        try
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(user.Email));  // The user's email
            message.From = new MailAddress(_smtpSettings.SenderEmail, _smtpSettings.SenderName);
            message.Subject = "User Registration Details";
            message.Body = MailTemplate.GetBody(user);
            message.IsBodyHtml = true;
            
            using var smtp = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                EnableSsl = true
            };
            await smtp.SendMailAsync(message);

            return Ok("User details sent successfully");
        }
        catch (SmtpException ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Error sending email: {ex.Message}");
        }
    }
}
