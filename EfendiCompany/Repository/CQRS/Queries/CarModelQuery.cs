using Core.Infrastructure;
using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface ICarModelQuery
{
    Task<IEnumerable<CarModel>> GetAllAsync();
    Task<ListResult<CarModel>> GetPaginationAsync(int offset, int limit);
    Task<CarModel> GetByIdAsync(int id);
}
public class CarModelQuery(IUnitOfWork _unitOfWork) : ICarModelQuery
{
    public async Task<IEnumerable<CarModel>> GetAllAsync()
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM CarModels
                                       WHERE IsDeleted = 0
                                       ORDER BY Id Desc";
            var result = await _unitOfWork.GetConnection().QueryAsync<CarModel>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ListResult<CarModel>> GetPaginationAsync(int offset, int limit)
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM CarModels
                                   WHERE IsDeleted = 0
                                   ORDER BY Id Desc
                                   LIMIT @Limit OFFSET @Offset;

                                   SELECT COUNT(*) TotalCount FROM CarModels WHERE IsDeleted = 0";
            using (var multi = await _unitOfWork.GetConnection().QueryMultipleAsync(_getAllSql, new { Offset = offset, Limit = limit }, _unitOfWork.GetTransaction()))
            {
                var aboutUsList = (await multi.ReadAsync<CarModel>()).ToList();
                var totalCount = await multi.ReadFirstAsync<int>();

                var result = new ListResult<CarModel>
                {
                    Data = aboutUsList,
                    TotalCount = totalCount
                };

                return result;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CarModel> GetByIdAsync(int id)
    {
        try
        {
            string _getByIdSql = $@"SELECT * FROM CarModels
                                       WHERE IsDeleted = 0 AND Id = {id}";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<CarModel>(_getByIdSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}

