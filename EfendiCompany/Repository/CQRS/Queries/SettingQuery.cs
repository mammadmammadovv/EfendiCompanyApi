using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface ISettingQuery
{
    public Task<Settings> GetAsync();
}
public class SettingQuery(IUnitOfWork _unitOfWork) : ISettingQuery
{
    public async Task<Settings> GetAsync()
    {
        try
        {
            var getSql = "SELECT * FROM Settings";


            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Settings>(getSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}

