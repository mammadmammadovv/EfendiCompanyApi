using Core.Infrastructure;
using Core.Models;
using Repository.Repositories;

namespace Services.Services;
public interface IProductService
{
    Task PostAsync(Product model);
    Task PutAsync(Product model);
    Task DeleteAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetAllParentsAsync();
    Task<ListResult<Product>> GetPaginationAsync(int offset, int limit);
    Task<Product> GetByIdAsync(int id);
}
public class ProductService(IProductRepository _repository) : IProductService
{
    public async Task PostAsync(Product model)
    {
        await _repository.PostAsync(model);
    }

    public async Task PutAsync(Product model)
    {
        await _repository.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    public async Task<IEnumerable<Product>> GetAllParentsAsync()
    {
        return await _repository.GetAllParentsAsync();
    }

    public async Task<ListResult<Product>> GetPaginationAsync(int offset, int limit)
    {
        return await _repository.GetPaginationAsync(offset, limit);
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}

