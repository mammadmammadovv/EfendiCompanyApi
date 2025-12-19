using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface ILanguageQuery
{
    Task<IEnumerable<Language>> GetAllAsync();
    Task<IEnumerable<Language>> GetActiveAsync();
    Task<Language?> GetByCodeAsync(string code);
    Task<Language?> GetByIdAsync(int id);
}

public class LanguageQuery(IUnitOfWork _unitOfWork) : ILanguageQuery
{
    public async Task<IEnumerable<Language>> GetAllAsync()
    {
        try
        {
            string _getAllSql = @"SELECT * FROM Languages
                                  ORDER BY Id Desc";

            var result = await _unitOfWork.GetConnection().QueryAsync<Language>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Language>> GetActiveAsync()
    {
        try
        {
            string _getActiveSql = @"SELECT * FROM Languages
                                     WHERE IsActive = 1
                                     ORDER BY Id Desc";

            var result = await _unitOfWork.GetConnection().QueryAsync<Language>(_getActiveSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Language?> GetByCodeAsync(string code)
    {
        try
        {
            string _getByCodeSql = @"SELECT * FROM Languages
                                      WHERE Code = @Code
                                      LIMIT 1";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Language>(_getByCodeSql, 
                new { Code = code }, 
                _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Language?> GetByIdAsync(int id)
    {
        try
        {
            string _getByIdSql = @"SELECT * FROM Languages
                                   WHERE Id = @Id
                                   LIMIT 1";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Language>(_getByIdSql, 
                new { Id = id }, 
                _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

