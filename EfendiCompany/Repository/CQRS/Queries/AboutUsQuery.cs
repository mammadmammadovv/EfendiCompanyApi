using Core.Infrastructure;
using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface IAboutUsQuery
{
    Task<IEnumerable<AboutUs>> GetAllAsync();
    Task<ListResult<AboutUs>> GetPaginationAsync(int offset, int limit);
    Task<AboutUs> GetByIdAsync(int id);
}
public class AboutUsQuery(IUnitOfWork _unitOfWork) : IAboutUsQuery
{
    public async Task<IEnumerable<AboutUs>> GetAllAsync()
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM AboutUs
                                       WHERE IsDeleted = 0
                                       ORDER BY Id Desc";
            var result = await _unitOfWork.GetConnection().QueryAsync<AboutUs>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ListResult<AboutUs>> GetPaginationAsync(int offset, int limit)
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM AboutUs
                                   ORDER BY Id Desc
                                   LIMIT @Limit OFFSET @Offset;

                                   SELECT COUNT(*) TotalCount FROM AboutUs";
            using (var multi = await _unitOfWork.GetConnection().QueryMultipleAsync(_getAllSql, new { Offset = offset, Limit = limit }, _unitOfWork.GetTransaction()))
            {
                var aboutUsList = (await multi.ReadAsync<AboutUs>()).ToList();  
                var totalCount = await multi.ReadFirstAsync<int>();             

                var result = new ListResult<AboutUs>
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

    public async Task<AboutUs> GetByIdAsync(int id)
    {
        try
        {
            string _getByIdSql = $@"SELECT * FROM AboutUs
                                       WHERE IsDeleted = 0 AND Id = {id}";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<AboutUs>(_getByIdSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
