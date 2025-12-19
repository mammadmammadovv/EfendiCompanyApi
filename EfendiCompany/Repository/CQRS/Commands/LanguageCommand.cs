using Core.Exceptions;
using Core.Models;
using Dapper;
using Repository.CQRS.Queries;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;

public interface ILanguageCommand
{
    Task PostAsync(Language model);
    Task PutAsync(Language model);
    Task DeactivateAsync(int id);
}

public class LanguageCommand(IUnitOfWork _unitOfWork, ILanguageQuery _query) : ILanguageCommand
{
    public async Task PostAsync(Language model)
    {
        try
        {
            // Validation: Code cannot be empty
            if (string.IsNullOrWhiteSpace(model.Code))
            {
                throw new BadRequestException("Language code cannot be empty.");
            }

            // Validation: Language code must be unique
            var existingLanguage = await _query.GetByCodeAsync(model.Code);
            if (existingLanguage != null)
            {
                throw new BadRequestException($"Language with code '{model.Code}' already exists.");
            }

            string _addSql = @"INSERT INTO Languages (Code, Name, IsActive, CreatedDate)
                                VALUES (@Code, @Name, @IsActive, datetime('now'));";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, 
                new { Code = model.Code, Name = model.Name, IsActive = model.IsActive ? 1 : 0 }, 
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

    public async Task PutAsync(Language model)
    {
        try
        {
            // Validation: Code cannot be empty
            if (string.IsNullOrWhiteSpace(model.Code))
            {
                throw new BadRequestException("Language code cannot be empty.");
            }

            // Validation: Language code must be unique (excluding current record)
            var existingLanguage = await _query.GetByCodeAsync(model.Code);
            if (existingLanguage != null && existingLanguage.Id != model.Id)
            {
                throw new BadRequestException($"Language with code '{model.Code}' already exists.");
            }

            string _updateSql = @"UPDATE Languages
                                   SET Code = @Code,
                                       Name = @Name,
                                       IsActive = @IsActive
                                   WHERE Id = @Id";

            await _unitOfWork.GetConnection().QueryAsync(_updateSql, 
                new { Id = model.Id, Code = model.Code, Name = model.Name, IsActive = model.IsActive ? 1 : 0 }, 
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
            string _deactivateSql = @"UPDATE Languages
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
