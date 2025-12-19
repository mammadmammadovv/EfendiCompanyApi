using Core.Exceptions;
using Core.Models;
using Dapper;
using Repository.CQRS.Queries;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;

public interface ILocalizationCommand
{
    Task PostAsync(LocalizationResource model);
    Task PutAsync(LocalizationResource model);
    Task DeactivateAsync(int id);
    Task PostBulkAsync(IEnumerable<LocalizationResource> resources);
}

public class LocalizationCommand(IUnitOfWork _unitOfWork, ILanguageQuery _languageQuery) : ILocalizationCommand
{
    public async Task PostAsync(LocalizationResource model)
    {
        try
        {
            // Validate language existence and active status
            var language = await _languageQuery.GetByCodeAsync(model.LanguageCode);
            if (language == null)
            {
                throw new BadRequestException($"Language with code '{model.LanguageCode}' does not exist.");
            }

            if (!language.IsActive)
            {
                throw new BadRequestException($"Language with code '{model.LanguageCode}' is not active. Cannot create localization resource for inactive language.");
            }

            // Ensure IsActive is true by default (model defaults to true, but ensure it's set)
            model.IsActive = true;

            // Validate uniqueness: Check if active record with same Key+LanguageCode already exists
            string _checkSql = @"SELECT * FROM LocalizationResources
                                WHERE Key = @Key AND LanguageCode = @LanguageCode AND IsActive = 1
                                LIMIT 1";
            var existingActive = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<LocalizationResource>(_checkSql, 
                new { Key = model.Key, LanguageCode = model.LanguageCode }, 
                _unitOfWork.GetTransaction());
            if (existingActive != null)
            {
                throw new BadRequestException($"Active localization resource with key '{model.Key}' and language code '{model.LanguageCode}' already exists.");
            }

            string _addSql = @"INSERT INTO LocalizationResources (Key, Value, LanguageCode, CreatedDate, IsActive)
                                VALUES (@Key, @Value, @LanguageCode, datetime('now'), @IsActive);";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, 
                new { Key = model.Key, Value = model.Value, LanguageCode = model.LanguageCode, IsActive = model.IsActive ? 1 : 0 }, 
                _unitOfWork.GetTransaction());
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PostBulkAsync(IEnumerable<LocalizationResource> resources)
    {
        try
        {
            var resourcesList = resources.ToList();
            if (!resourcesList.Any())
                return;

            // Validate all languages before inserting
            var languageCodes = resourcesList.Select(r => r.LanguageCode).Distinct().ToList();
            foreach (var languageCode in languageCodes)
            {
                var language = await _languageQuery.GetByCodeAsync(languageCode);
                if (language == null)
                {
                    throw new BadRequestException($"Language with code '{languageCode}' does not exist.");
                }

                if (!language.IsActive)
                {
                    throw new BadRequestException($"Language with code '{languageCode}' is not active. Cannot create localization resources for inactive language.");
                }
            }

            // Ensure IsActive = true by default for all resources
            // Validate uniqueness for each resource
            foreach (var resource in resourcesList)
            {
                resource.IsActive = true;
                
                // Check for duplicate active records
                string _checkSql = @"SELECT * FROM LocalizationResources
                                    WHERE Key = @Key AND LanguageCode = @LanguageCode AND IsActive = 1
                                    LIMIT 1";
                var existingActive = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<LocalizationResource>(_checkSql, 
                    new { Key = resource.Key, LanguageCode = resource.LanguageCode }, 
                    _unitOfWork.GetTransaction());
                if (existingActive != null)
                {
                    throw new BadRequestException($"Active localization resource with key '{resource.Key}' and language code '{resource.LanguageCode}' already exists.");
                }
            }

            var values = string.Join(", ", resourcesList.Select(r => 
                $"('{r.Key.Replace("'", "''")}', '{r.Value.Replace("'", "''")}', '{r.LanguageCode}', datetime('now'), 1)"));

            string _addSql = $@"INSERT INTO LocalizationResources (Key, Value, LanguageCode, CreatedDate, IsActive)
                                VALUES {values};";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PutAsync(LocalizationResource model)
    {
        try
        {
            // Validate language existence and active status
            var language = await _languageQuery.GetByCodeAsync(model.LanguageCode);
            if (language == null)
            {
                throw new BadRequestException($"Language with code '{model.LanguageCode}' does not exist.");
            }

            if (!language.IsActive)
            {
                throw new BadRequestException($"Language with code '{model.LanguageCode}' is not active. Cannot update localization resource for inactive language.");
            }

            // Update Value only (IsActive is managed separately via DeactivateAsync)
            // By default, don't change IsActive unless explicitly set to false
            string _updateSql = @"UPDATE LocalizationResources
                                   SET Value = @Value
                                   WHERE Key = @Key AND LanguageCode = @LanguageCode";

            await _unitOfWork.GetConnection().QueryAsync(_updateSql, 
                new { Key = model.Key, Value = model.Value, LanguageCode = model.LanguageCode }, 
                _unitOfWork.GetTransaction());
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task DeactivateAsync(int id)
    {
        try
        {
            // Soft delete: Set IsActive = false instead of physically deleting
            string _deactivateSql = @"UPDATE LocalizationResources
                                      SET IsActive = 0
                                      WHERE Id = @Id";

            await _unitOfWork.GetConnection().QueryAsync(_deactivateSql, 
                new { Id = id }, 
                _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

