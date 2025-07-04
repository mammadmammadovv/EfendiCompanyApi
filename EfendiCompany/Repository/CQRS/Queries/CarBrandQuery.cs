using Core.Infrastructure;
using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface ICarBrandQuery
{
    Task<IEnumerable<CarBrand>> GetAllAsync();
    Task<ListResult<CarBrand>> GetPaginationAsync(int offset, int limit);
    Task<CarBrand> GetByIdAsync(int id);
}
public class CarBrandQuery(IUnitOfWork _unitOfWork) : ICarBrandQuery
{
    public async Task<IEnumerable<CarBrand>> GetAllAsync()
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM CarBrands
                                       WHERE IsDeleted = 0
                                       ORDER BY Id Desc";
            var result = await _unitOfWork.GetConnection().QueryAsync<CarBrand>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ListResult<CarBrand>> GetPaginationAsync(int offset, int limit)
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM CarBrands
                                   WHERE IsDeleted = 0
                                   ORDER BY Id Desc
                                   LIMIT @Limit OFFSET @Offset;

                                   SELECT COUNT(*) TotalCount FROM CarBrands WHERE IsDeleted = 0";
            using (var multi = await _unitOfWork.GetConnection().QueryMultipleAsync(_getAllSql, new { Offset = offset, Limit = limit }, _unitOfWork.GetTransaction()))
            {
                var aboutUsList = (await multi.ReadAsync<CarBrand>()).ToList();
                var totalCount = await multi.ReadFirstAsync<int>();

                var result = new ListResult<CarBrand>
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

    public async Task<CarBrand> GetByIdAsync(int id)
    {
        try
        {
            string _getByIdSql = $@"SELECT * FROM CarBrands
                                       WHERE IsDeleted = 0 AND Id = {id}";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<CarBrand>(_getByIdSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}

