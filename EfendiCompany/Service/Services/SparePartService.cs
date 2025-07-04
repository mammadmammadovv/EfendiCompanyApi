using Core.Infrastructure;
using Core.Models;
using Repository.Repositories;

namespace Services.Services;

public interface ISparePartService
{
    Task PostAsync(SparePart model);
    Task PutAsync(SparePart model);
    Task DeleteAsync(int id);
    Task<IEnumerable<SparePart>> GetAllAsync();
    Task<ListResult<SparePart>> GetPaginationAsync(int offset, int limit);
    Task<SparePart> GetByIdAsync(int id);
}
public class SparePartService(ISparePartRepository _repository) : ISparePartService
{
    public async Task PostAsync(SparePart model)
    {
        await _repository.PostAsync(model);
    }

    public async Task PutAsync(SparePart model)
    {
        await _repository.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<SparePart>> GetAllAsync()
    {
        var res = await _repository.GetAllAsync();
        return res;
    }

    public async Task<ListResult<SparePart>> GetPaginationAsync(int offset, int limit)
    {
        var res = await _repository.GetPaginationAsync(offset, limit);
        return res;
    }

    public async Task<SparePart> GetByIdAsync(int id)
    {
        var res = await _repository.GetByIdAsync(id);
        return res;
    }
}

