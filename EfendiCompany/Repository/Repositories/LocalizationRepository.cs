using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;

public interface ILocalizationRepository : ILocalizationCommand, ILocalizationQuery { }

public class LocalizationRepository(ILocalizationCommand _command, ILocalizationQuery _query) : ILocalizationRepository
{
    public async Task PostAsync(LocalizationResource model)
    {
        await _command.PostAsync(model);
    }

    public async Task PostBulkAsync(IEnumerable<LocalizationResource> resources)
    {
        await _command.PostBulkAsync(resources);
    }

    public async Task PutAsync(LocalizationResource model)
    {
        await _command.PutAsync(model);
    }

    public async Task DeactivateAsync(int id)
    {
        await _command.DeactivateAsync(id);
    }

    public async Task<string?> GetTranslationAsync(string key, string languageCode)
    {
        return await _query.GetTranslationAsync(key, languageCode);
    }

    public async Task<Dictionary<string, string>> GetAllTranslationsAsync(string languageCode)
    {
        return await _query.GetAllTranslationsAsync(languageCode);
    }

    public async Task<LocalizationResource?> GetResourceAsync(string key, string languageCode)
    {
        return await _query.GetResourceAsync(key, languageCode);
    }

    public async Task<LocalizationResource?> GetResourceAnyStatusAsync(string key, string languageCode)
    {
        return await _query.GetResourceAnyStatusAsync(key, languageCode);
    }

    public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
    {
        return await _query.GetAllLanguagesAsync();
    }

    public async Task<IEnumerable<LocalizationResource>> GetAllResourcesAsync()
    {
        return await _query.GetAllResourcesAsync();
    }

    public async Task<IEnumerable<LocalizationResource>> GetAllResourcesAdminAsync(bool includeInactive = false)
    {
        return await _query.GetAllResourcesAdminAsync(includeInactive);
    }
}

