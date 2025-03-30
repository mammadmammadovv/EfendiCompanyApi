using Core.Infrastructure;
using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface IProductQuery
{

    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetAllParentsAsync();
    Task<ListResult<Product>> GetPaginationAsync(int offset, int limit);
    Task<Product> GetByIdAsync(int id);
}
public class ProductQuery(IUnitOfWork _unitOfWork) : IProductQuery
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM Products
                                       WHERE IsDeleted = 0
                                       ORDER BY Id Desc";
            var result = await _unitOfWork.GetConnection().QueryAsync<Product>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Product>> GetAllParentsAsync()
    {
        try
        {
            string _getAllParentsSql = $@"SELECT * FROM Products
                                       WHERE IsDeleted = 0 AND ParentProductId = 0
                                       ORDER BY Id Desc";
            var result = await _unitOfWork.GetConnection().QueryAsync<Product>(_getAllParentsSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ListResult<Product>> GetPaginationAsync(int offset, int limit)
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM Products
                                   WHERE IsDeleted = 0
                                   ORDER BY Id Desc
                                   LIMIT @Limit OFFSET @Offset;

                                   SELECT COUNT(*) TotalCount FROM Products WHERE IsDeleted = 0";
            using (var multi = await _unitOfWork.GetConnection().QueryMultipleAsync(_getAllSql, new { Offset = offset, Limit = limit }, _unitOfWork.GetTransaction()))
            {
                var aboutUsList = (await multi.ReadAsync<Product>()).ToList();
                var totalCount = await multi.ReadFirstAsync<int>();

                var result = new ListResult<Product>
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

    public async Task<Product> GetByIdAsync(int id)
    {
        try
        {
            string _getByIdSql = $@"SELECT * FROM Products
                                       WHERE IsDeleted = 0 AND Id = {id}";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Product>(_getByIdSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}

