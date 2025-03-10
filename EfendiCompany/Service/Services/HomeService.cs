using Core.Models;
using Repository.Repositories;

namespace Services.Services;
public interface IHomeService
{
    public Task PutAsync(Home model);
    public Task<Home> GetAsync();
}
public class HomeService(IHomeRepository _repo) : IHomeService
{
    public async Task PutAsync(Home model)
    {
        await _repo.PutAsync(model);
    }

    public async Task<Home> GetAsync()
    {
        var res = await _repo.GetAsync();
        return res;
    }
}

