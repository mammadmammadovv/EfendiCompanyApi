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
        //	mammadmammadovv@gmail.com
        await _service.SendEmailAsync("rufet.ismayilov.1999@gmail.com", email.Subject + " - " + email.Name + " " + email.Surname, email.Body);
        return Ok();
    }
}
