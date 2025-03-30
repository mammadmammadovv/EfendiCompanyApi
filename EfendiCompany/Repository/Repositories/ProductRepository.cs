using Core.Infrastructure;
using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;

public interface IProductRepository : IProductCommand, IProductQuery { }
public class ProductRepository(IProductCommand _command, IProductQuery _query) : IProductRepository
{
    public async Task PostAsync(Product model)
    {
        await _command.PostAsync(model);
    }

    public async Task PutAsync(Product model)
    {
        await _command.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _command.DeleteAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _query.GetAllAsync();
    }

    public async Task<IEnumerable<Product>> GetAllParentsAsync()
    {
        return await _query.GetAllParentsAsync();
    }

    public async Task<ListResult<Product>> GetPaginationAsync(int offset, int limit)
    {
        return await _query.GetPaginationAsync(offset, limit);
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _query.GetByIdAsync(id);
    }
}

