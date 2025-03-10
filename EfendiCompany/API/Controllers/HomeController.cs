using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HomeController(IHomeService _service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetAsync();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Home model)
    {
        await _service.PutAsync(model);
        return Ok();
    }
}
