using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface ICarModelImageQuery
{
    Task<IEnumerable<CarModelImage>> GetByCarModelIdAsync(int carModelId);
    Task<CarModelImage> GetByIdAsync(int id);
}

public class CarModelImageQuery(IUnitOfWork _unitOfWork) : ICarModelImageQuery
{
    public async Task<IEnumerable<CarModelImage>> GetByCarModelIdAsync(int carModelId)
    {
        try
        {
            string _getByCarModelIdSql = $@"SELECT * FROM CarModelImages
                                       WHERE IsDeleted = 0 AND CarModelId = {carModelId}
                                       ORDER BY DisplayOrder ASC, Id ASC";

            var result = await _unitOfWork.GetConnection().QueryAsync<CarModelImage>(_getByCarModelIdSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CarModelImage> GetByIdAsync(int id)
    {
        try
        {
            string _getByIdSql = $@"SELECT * FROM CarModelImages
                                       WHERE IsDeleted = 0 AND Id = {id}";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<CarModelImage>(_getByIdSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

