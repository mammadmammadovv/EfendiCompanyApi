using Core.Models;
using Repository.Repositories;

namespace Services.Services;

public interface IServicesService
{
    Task PostAsync(Service model);
    Task PutAsync(Service model);
    Task DeleteAsync(int id);
    Task<IEnumerable<Service>> GetAllAsync();
    Task<Service> GetByIdAsync(int id);
}

public class ServicesService(IServicesRepository _repo) : IServicesService
{
    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }

    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Service> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task PostAsync(Service model)
    {
        await _repo.PostAsync(model);
    }

    public async Task PutAsync(Service model)
    {
        await _repo.PutAsync(model);
    }
}
