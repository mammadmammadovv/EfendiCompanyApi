using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface IServicesQuery
{
    Task<IEnumerable<Service>> GetAllAsync();
    Task<Service> GetByIdAsync(int id);
}
public class ServicesQuery(IUnitOfWork _unitOfWork) : IServicesQuery
{
    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM Services
                                       WHERE IsDeleted = 0
                                       ORDER BY Id Desc";
            var result = await _unitOfWork.GetConnection().QueryAsync<Service>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Service> GetByIdAsync(int id)
    {
        try
        {
            string _getByIdSql = $@"SELECT * FROM Services
                                       WHERE IsDeleted = 0 AND Id = {id}";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Service>(_getByIdSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
