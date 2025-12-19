using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface ILocalizationQuery
{
    Task<string?> GetTranslationAsync(string key, string languageCode);
    Task<Dictionary<string, string>> GetAllTranslationsAsync(string languageCode);
    Task<LocalizationResource?> GetResourceAsync(string key, string languageCode);
    Task<LocalizationResource?> GetResourceAnyStatusAsync(string key, string languageCode);
    Task<IEnumerable<Language>> GetAllLanguagesAsync();
    Task<IEnumerable<LocalizationResource>> GetAllResourcesAsync();
    Task<IEnumerable<LocalizationResource>> GetAllResourcesAdminAsync(bool includeInactive = false);
}

public class LocalizationQuery(IUnitOfWork _unitOfWork) : ILocalizationQuery
{
    public async Task<string?> GetTranslationAsync(string key, string languageCode)
    {
        try
        {
            // Only return active resources
            string _getSql = @"SELECT Value FROM LocalizationResources
                                WHERE Key = @Key AND LanguageCode = @LanguageCode AND IsActive = 1
                                LIMIT 1";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<string>(_getSql, 
                new { Key = key, LanguageCode = languageCode }, 
                _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Dictionary<string, string>> GetAllTranslationsAsync(string languageCode)
    {
        try
        {
            // Only return active resources
            string _getAllSql = @"SELECT Key, Value FROM LocalizationResources
                                    WHERE LanguageCode = @LanguageCode AND IsActive = 1
                                    ORDER BY Key";

            var results = await _unitOfWork.GetConnection().QueryAsync<(string Key, string Value)>(_getAllSql, 
                new { LanguageCode = languageCode }, 
                _unitOfWork.GetTransaction());
            return results.ToDictionary(r => r.Key, r => r.Value);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<LocalizationResource?> GetResourceAsync(string key, string languageCode)
    {
        try
        {
            // Only return active resources
            string _getSql = @"SELECT * FROM LocalizationResources
                                WHERE Key = @Key AND LanguageCode = @LanguageCode AND IsActive = 1
                                LIMIT 1";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<LocalizationResource>(_getSql, 
                new { Key = key, LanguageCode = languageCode }, 
                _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<LocalizationResource?> GetResourceAnyStatusAsync(string key, string languageCode)
    {
        try
        {
            // Return resource regardless of IsActive status (for validation purposes)
            string _getSql = @"SELECT * FROM LocalizationResources
                                WHERE Key = @Key AND LanguageCode = @LanguageCode
                                LIMIT 1";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<LocalizationResource>(_getSql, 
                new { Key = key, LanguageCode = languageCode }, 
                _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
    {
        try
        {
            string _getAllSql = @"SELECT * FROM Languages
                                  WHERE IsActive = 1
                                  ORDER BY Id";

            var result = await _unitOfWork.GetConnection().QueryAsync<Language>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<LocalizationResource>> GetAllResourcesAsync()
    {
        try
        {
            // Only return active resources by default
            string _getAllSql = @"SELECT * FROM LocalizationResources
                                  WHERE IsActive = 1
                                  ORDER BY Key, LanguageCode";

            var result = await _unitOfWork.GetConnection().QueryAsync<LocalizationResource>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<LocalizationResource>> GetAllResourcesAdminAsync(bool includeInactive = false)
    {
        try
        {
            // Admin query: can include inactive records if requested
            string _getAllSql = includeInactive
                ? @"SELECT * FROM LocalizationResources
                    ORDER BY Key, LanguageCode"
                : @"SELECT * FROM LocalizationResources
                    WHERE IsActive = 1
                    ORDER BY Key, LanguageCode";

            var result = await _unitOfWork.GetConnection().QueryAsync<LocalizationResource>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

