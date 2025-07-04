using Core.Infrastructure;
using Core.Models;
using Repository.Repositories;

namespace Services.Services;

public interface ICarModelService
{
    Task PostAsync(CarModel model);
    Task PutAsync(CarModel model);
    Task DeleteAsync(int id);
    Task<IEnumerable<CarModel>> GetAllAsync();
    Task<ListResult<CarModel>> GetPaginationAsync(int offset, int limit);
    Task<CarModel> GetByIdAsync(int id);
}
public class CarModelService(ICarModelRepository _repository) : ICarModelService
{
    public async Task PostAsync(CarModel model)
    {
        await _repository.PostAsync(model);
    }

    public async Task PutAsync(CarModel model)
    {
        await _repository.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<CarModel>> GetAllAsync()
    {
        var res = await _repository.GetAllAsync();
        return res;
    }

    public async Task<ListResult<CarModel>> GetPaginationAsync(int offset, int limit)
    {
        var res = await _repository.GetPaginationAsync(offset, limit);
        return res;
    }

    public async Task<CarModel> GetByIdAsync(int id)
    {
        var res = await _repository.GetByIdAsync(id);
        return res;
    }
}

