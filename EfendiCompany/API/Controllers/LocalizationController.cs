using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocalizationController(ILocalizationService _service) : ControllerBase
{
    /// <summary>
    /// Get translation by key and language code
    /// Example: GET /api/localization?lang=az&key=DashboardPage.Welcome
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetTranslation([FromQuery] string lang, [FromQuery] string key)
    {
        if (string.IsNullOrWhiteSpace(lang) || string.IsNullOrWhiteSpace(key))
        {
            return BadRequest("Language code and key are required.");
        }

        var translation = await _service.GetTranslationAsync(key, lang.ToLower());
        return Ok(translation);
    }

    /// <summary>
    /// Get all translations for a given language
    /// Example: GET /api/localization/all?lang=az
    /// </summary>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllTranslations([FromQuery] string lang)
    {
        if (string.IsNullOrWhiteSpace(lang))
        {
            return BadRequest("Language code is required.");
        }

        var translations = await _service.GetAllTranslationsAsync(lang.ToLower());
        return Ok(translations);
    }

    /// <summary>
    /// Add a single localization resource
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] LocalizationResource model)
    {
        if (string.IsNullOrWhiteSpace(model.Key) || string.IsNullOrWhiteSpace(model.LanguageCode))
        {
            return BadRequest("Key and LanguageCode are required.");
        }

        model.LanguageCode = model.LanguageCode.ToLower();
        await _service.PostAsync(model);
        return Ok();
    }

    /// <summary>
    /// Add multiple localization resources at once
    /// </summary>
    [HttpPost("bulk")]
    public async Task<IActionResult> PostBulk([FromBody] IEnumerable<LocalizationResource> resources)
    {
        var resourcesList = resources.ToList();
        if (!resourcesList.Any())
        {
            return BadRequest("At least one resource is required.");
        }

        foreach (var resource in resourcesList)
        {
            resource.LanguageCode = resource.LanguageCode.ToLower();
        }

        await _service.PostBulkAsync(resourcesList);
        return Ok();
    }

    /// <summary>
    /// Update a localization resource
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] LocalizationResource model)
    {
        if (string.IsNullOrWhiteSpace(model.Key) || string.IsNullOrWhiteSpace(model.LanguageCode))
        {
            return BadRequest("Key and LanguageCode are required.");
        }

        model.LanguageCode = model.LanguageCode.ToLower();
        await _service.PutAsync(model);
        return Ok();
    }

    /// <summary>
    /// Deactivate a localization resource by ID (soft delete)
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Deactivate(int id)
    {
        try
        {
            await _service.DeactivateAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while deactivating the localization resource.");
        }
    }

    /// <summary>
    /// Get all available languages
    /// </summary>
    [HttpGet("languages")]
    public async Task<IActionResult> GetAllLanguages()
    {
        var languages = await _service.GetAllLanguagesAsync();
        return Ok(languages);
    }

    /// <summary>
    /// Get all localization resources (for admin purposes)
    /// </summary>
    [HttpGet("resources")]
    public async Task<IActionResult> GetAllResources([FromQuery] bool includeInactive = false)
    {
        var resources = await _service.GetAllResourcesAdminAsync(includeInactive);
        return Ok(resources);
    }
}

