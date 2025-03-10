using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface IHomeQuery
{
    public Task<Home> GetAsync();
}
public class HomeQuery(IUnitOfWork _unitOfWork) : IHomeQuery
{
    public async Task<Home> GetAsync()
    {
        try
        {
            var getSql = "SELECT * FROM Home";


            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Home>(getSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}

