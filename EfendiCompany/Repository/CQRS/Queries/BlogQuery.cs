using Core.Infrastructure;
using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface IBlogQuery
{
    Task<IEnumerable<Blog>> GetAllAsync();
    Task<ListResult<Blog>> GetPaginationAsync(int offset, int limit);
    Task<Blog> GetByIdAsync(int id);
}
public class BlogQuery(IUnitOfWork _unitOfWork) : IBlogQuery
{
    public async Task<IEnumerable<Blog>> GetAllAsync()
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM Blogs
                                       WHERE IsDeleted = 0
                                       ORDER BY Id Desc";
            var result = await _unitOfWork.GetConnection().QueryAsync<Blog>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ListResult<Blog>> GetPaginationAsync(int offset, int limit)
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM Blogs
                                   WHERE IsDeleted = 0
                                   ORDER BY Id Desc
                                   LIMIT @Limit OFFSET @Offset;

                                   SELECT COUNT(*) TotalCount FROM Blogs WHERE IsDeleted = 0";
            using (var multi = await _unitOfWork.GetConnection().QueryMultipleAsync(_getAllSql, new { Offset = offset, Limit = limit }, _unitOfWork.GetTransaction()))
            {
                var aboutUsList = (await multi.ReadAsync<Blog>()).ToList();
                var totalCount = await multi.ReadFirstAsync<int>();

                var result = new ListResult<Blog>
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

    public async Task<Blog> GetByIdAsync(int id)
    {
        try
        {
            string _getByIdSql = $@"SELECT * FROM Blogs
                                       WHERE IsDeleted = 0 AND Id = {id}";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Blog>(_getByIdSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}

