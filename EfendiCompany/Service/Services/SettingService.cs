using Core.Models;
using Repository.Repositories;

namespace Services.Services;
public interface ISettingService
{
    public Task<Settings> GetAsync();
    public Task PutAsync(Settings model);
}
public class SettingService(ISettingRepository _repo) : ISettingService
{
    public async Task<Settings> GetAsync()
    {
        var res = await _repo.GetAsync();
        return res;
    }

    public async Task PutAsync(Settings model)
    {
        await _repo.PutAsync(model);
    }
}

