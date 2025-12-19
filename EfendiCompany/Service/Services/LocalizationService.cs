using Core.Models;
using Repository.Repositories;

namespace Services.Services;

public interface ILocalizationService
{
    Task<string> GetTranslationAsync(string key, string languageCode);
    Task<Dictionary<string, string>> GetAllTranslationsAsync(string languageCode);
    Task PostAsync(LocalizationResource model);
    Task PostBulkAsync(IEnumerable<LocalizationResource> resources);
    Task PutAsync(LocalizationResource model);
    Task DeactivateAsync(int id);
    Task<IEnumerable<Language>> GetAllLanguagesAsync();
    Task<IEnumerable<LocalizationResource>> GetAllResourcesAsync();
    Task<IEnumerable<LocalizationResource>> GetAllResourcesAdminAsync(bool includeInactive = false);
}

public class LocalizationService(ILocalizationRepository _repository) : ILocalizationService
{
    private const string DefaultLanguageCode = "en";

    public async Task<string> GetTranslationAsync(string key, string languageCode)
    {
        // Try to get translation in requested language
        var translation = await _repository.GetTranslationAsync(key, languageCode);
        
        if (!string.IsNullOrWhiteSpace(translation))
        {
            return translation;
        }

        // Fallback to English if requested language is not English
        if (languageCode.ToLower() != DefaultLanguageCode.ToLower())
        {
            translation = await _repository.GetTranslationAsync(key, DefaultLanguageCode);
            if (!string.IsNullOrWhiteSpace(translation))
            {
                return translation;
            }
        }

        // If still not found, return the key itself
        return key;
    }

    public async Task<Dictionary<string, string>> GetAllTranslationsAsync(string languageCode)
    {
        var translations = await _repository.GetAllTranslationsAsync(languageCode);
        
        // If no translations found for requested language and it's not English, try English
        if (!translations.Any() && languageCode.ToLower() != DefaultLanguageCode.ToLower())
        {
            translations = await _repository.GetAllTranslationsAsync(DefaultLanguageCode);
        }

        return translations;
    }

    public async Task PostAsync(LocalizationResource model)
    {
        await _repository.PostAsync(model);
    }

    public async Task PostBulkAsync(IEnumerable<LocalizationResource> resources)
    {
        await _repository.PostBulkAsync(resources);
    }

    public async Task PutAsync(LocalizationResource model)
    {
        await _repository.PutAsync(model);
    }

    public async Task DeactivateAsync(int id)
    {
        await _repository.DeactivateAsync(id);
    }

    public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
    {
        return await _repository.GetAllLanguagesAsync();
    }

    public async Task<IEnumerable<LocalizationResource>> GetAllResourcesAsync()
    {
        return await _repository.GetAllResourcesAsync();
    }

    public async Task<IEnumerable<LocalizationResource>> GetAllResourcesAdminAsync(bool includeInactive = false)
    {
        return await _repository.GetAllResourcesAdminAsync(includeInactive);
    }
}

