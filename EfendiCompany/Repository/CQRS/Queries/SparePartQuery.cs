using Core.Infrastructure;
using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface ISparePartQuery
{
    Task<IEnumerable<SparePart>> GetAllAsync();
    Task<ListResult<SparePart>> GetPaginationAsync(int offset, int limit);
    Task<SparePart> GetByIdAsync(int id);
}
public class SparePartQuery(IUnitOfWork _unitOfWork) : ISparePartQuery
{
    public async Task<IEnumerable<SparePart>> GetAllAsync()
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM SpareParts
                                       WHERE IsDeleted = 0
                                       ORDER BY Id Desc";
            var result = await _unitOfWork.GetConnection().QueryAsync<SparePart>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ListResult<SparePart>> GetPaginationAsync(int offset, int limit)
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM SpareParts
                                   WHERE IsDeleted = 0
                                   ORDER BY Id Desc
                                   LIMIT @Limit OFFSET @Offset;

                                   SELECT COUNT(*) TotalCount FROM CarModels WHERE IsDeleted = 0";
            using (var multi = await _unitOfWork.GetConnection().QueryMultipleAsync(_getAllSql, new { Offset = offset, Limit = limit }, _unitOfWork.GetTransaction()))
            {
                var aboutUsList = (await multi.ReadAsync<SparePart>()).ToList();
                var totalCount = await multi.ReadFirstAsync<int>();

                var result = new ListResult<SparePart>
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

    public async Task<SparePart> GetByIdAsync(int id)
    {
        try
        {
            string _getByIdSql = $@"SELECT * FROM SpareParts
                                       WHERE IsDeleted = 0 AND Id = {id}";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<SparePart>(_getByIdSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}

