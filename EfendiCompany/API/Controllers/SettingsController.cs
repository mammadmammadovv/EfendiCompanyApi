using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SettingsController(ISettingService _service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetAsync();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Settings model)
    {
        await _service.PutAsync(model);
        return Ok();
    }
}
