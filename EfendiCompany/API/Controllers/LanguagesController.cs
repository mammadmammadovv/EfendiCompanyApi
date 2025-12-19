using Core.Exceptions;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguagesController(ILanguageService _service) : ControllerBase
{
    /// <summary>
    /// Create a new language
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Language model)
    {
        try
        {
            await _service.PostAsync(model);
            return Ok();
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while creating the language.");
        }
    }

    /// <summary>
    /// Update an existing language
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Language model)
    {
        try
        {
            model.Id = id;
            await _service.PutAsync(model);
            return Ok();
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while updating the language.");
        }
    }

    /// <summary>
    /// Get all languages
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Get all active languages
    /// </summary>
    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var result = await _service.GetActiveAsync();
        return Ok(result);
    }

    /// <summary>
    /// Get language by code
    /// </summary>
    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var result = await _service.GetByCodeAsync(code);
        if (result == null)
        {
            return NotFound($"Language with code '{code}' not found.");
        }
        return Ok(result);
    }

    /// <summary>
    /// Deactivate a language (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeactivateAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while deactivating the language.");
        }
    }
}
