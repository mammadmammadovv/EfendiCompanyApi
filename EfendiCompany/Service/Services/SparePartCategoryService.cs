using Core.Infrastructure;
using Core.Models;
using Repository.Repositories;

namespace Services.Services;

public interface ISparePartCategoryService
{
    Task PostAsync(SparePartCategory model);
    Task PutAsync(SparePartCategory model);
    Task DeleteAsync(int id);
    Task<IEnumerable<SparePartCategory>> GetAllAsync();
    Task<ListResult<SparePartCategory>> GetPaginationAsync(int offset, int limit);
    Task<SparePartCategory> GetByIdAsync(int id);
}
public class SparePartCategoryService(ISparePartCategoryRepository _repository) : ISparePartCategoryService
{
    public async Task PostAsync(SparePartCategory model)
    {
        await _repository.PostAsync(model);
    }

    public async Task PutAsync(SparePartCategory model)
    {
        await _repository.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<SparePartCategory>> GetAllAsync()
    {
        var res = await _repository.GetAllAsync();
        return res;
    }

    public async Task<ListResult<SparePartCategory>> GetPaginationAsync(int offset, int limit)
    {
        var res = await _repository.GetPaginationAsync(offset, limit);
        return res;
    }

    public async Task<SparePartCategory> GetByIdAsync(int id)
    {
        var res = await _repository.GetByIdAsync(id);
        return res;
    }
}

