using Core.Models;
using GPS.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmailController(IEmailService _service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendEmailAsync([FromBody] EmailTemplate email)
    {
        var updatedBody = $"{email.Name} {email.Surname}\n{email.PhoneNumber}\n{email.Email}" + "\n" + email.Body;

        await _service.SendEmailAsync("rufet.ismayilov.1999@gmail.com", email.Subject, updatedBody);
        return Ok();
    }
}
