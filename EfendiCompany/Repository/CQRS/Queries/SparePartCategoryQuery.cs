using Core.Infrastructure;
using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface ISparePartCategoryQuery
{
    Task<IEnumerable<SparePartCategory>> GetAllAsync();
    Task<ListResult<SparePartCategory>> GetPaginationAsync(int offset, int limit);
    Task<SparePartCategory> GetByIdAsync(int id);
}
public class SparePartCategoryQuery(IUnitOfWork _unitOfWork) : ISparePartCategoryQuery
{
    public async Task<IEnumerable<SparePartCategory>> GetAllAsync()
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM SparePartCategories
                                       WHERE IsDeleted = 0
                                       ORDER BY Id Desc";
            var result = await _unitOfWork.GetConnection().QueryAsync<SparePartCategory>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ListResult<SparePartCategory>> GetPaginationAsync(int offset, int limit)
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM SparePartCategories
                                   WHERE IsDeleted = 0
                                   ORDER BY Id Desc
                                   LIMIT @Limit OFFSET @Offset;

                                   SELECT COUNT(*) TotalCount FROM SparePartCategories WHERE IsDeleted = 0";
            using (var multi = await _unitOfWork.GetConnection().QueryMultipleAsync(_getAllSql, new { Offset = offset, Limit = limit }, _unitOfWork.GetTransaction()))
            {
                var aboutUsList = (await multi.ReadAsync<SparePartCategory>()).ToList();
                var totalCount = await multi.ReadFirstAsync<int>();

                var result = new ListResult<SparePartCategory>
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

    public async Task<SparePartCategory> GetByIdAsync(int id)
    {
        try
        {
            string _getByIdSql = $@"SELECT * FROM SparePartCategories
                                       WHERE IsDeleted = 0 AND Id = {id}";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<SparePartCategory>(_getByIdSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}

