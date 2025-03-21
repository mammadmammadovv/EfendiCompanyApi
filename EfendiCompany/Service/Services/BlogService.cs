using Core.Infrastructure;
using Core.Models;
using Repository.Repositories;

namespace Services.Services;

public interface IBlogService
{
    Task PostAsync(Blog model);
    Task PutAsync(Blog model);
    Task DeleteAsync(int id);
    Task<IEnumerable<Blog>> GetAllAsync();
    Task<ListResult<Blog>> GetPaginationAsync(int offset, int limit);
    Task<Blog> GetByIdAsync(int id);
}
public class BlogService(IBlogRepository _repository) : IBlogService
{
    public async Task PostAsync(Blog model)
    {
        await _repository.PostAsync(model);
    }

    public async Task PutAsync(Blog model)
    {
        await _repository.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Blog>> GetAllAsync()
    {
        var res = await _repository.GetAllAsync();
        return res;
    }

    public async Task<ListResult<Blog>> GetPaginationAsync(int offset, int limit)
    {
        var res = await _repository.GetPaginationAsync(offset, limit);
        return res;
    }

    public async Task<Blog> GetByIdAsync(int id)
    {
        var res = await _repository.GetByIdAsync(id);
        return res;
    }
}

