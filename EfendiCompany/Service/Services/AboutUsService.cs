using Core.Infrastructure;
using Core.Models;
using Repository.Repositories;

namespace Services.Services;

public interface IAboutUsService
{
    Task PostAsync(AboutUs model);
    Task PutAsync(AboutUs model);
    Task DeleteAsync(int id);
    Task<IEnumerable<AboutUs>> GetAllAsync();
    Task<ListResult<AboutUs>> GetPaginationAsync(int offset, int limit);
    Task<AboutUs> GetByIdAsync(int id);
}
public class AboutUsService(IAboutUsRepository _repo) : IAboutUsService
{
    public async Task PostAsync(AboutUs model)
    {
        await _repo.PostAsync(model);
    }

    public async Task PutAsync(AboutUs model)
    {
        await _repo.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }

    public async Task<IEnumerable<AboutUs>> GetAllAsync()
    {
        var res = await _repo.GetAllAsync();
        return res;
    }

    public async Task<ListResult<AboutUs>> GetPaginationAsync(int offset, int limit)
    {
        var res = await _repo.GetPaginationAsync(offset, limit);
        return res;
    }

    public async Task<AboutUs> GetByIdAsync(int id)
    {
        var res = await _repo.GetByIdAsync(id);
        return res;
    }
}

