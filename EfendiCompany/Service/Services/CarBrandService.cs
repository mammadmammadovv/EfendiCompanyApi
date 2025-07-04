using Core.Infrastructure;
using Core.Models;
using Repository.Repositories;

namespace Services.Services;

public interface ICarBrandService
{
    Task PostAsync(CarBrand model);
    Task PutAsync(CarBrand model);
    Task DeleteAsync(int id);
    Task<IEnumerable<CarBrand>> GetAllAsync();
    Task<ListResult<CarBrand>> GetPaginationAsync(int offset, int limit);
    Task<CarBrand> GetByIdAsync(int id);
}
public class CarBrandService(ICarBrandRepository _repository) : ICarBrandService
{
    public async Task PostAsync(CarBrand model)
    {
        await _repository.PostAsync(model);
    }

    public async Task PutAsync(CarBrand model)
    {
        await _repository.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<CarBrand>> GetAllAsync()
    {
        var res = await _repository.GetAllAsync();
        return res;
    }

    public async Task<ListResult<CarBrand>> GetPaginationAsync(int offset, int limit)
    {
        var res = await _repository.GetPaginationAsync(offset, limit);
        return res;
    }

    public async Task<CarBrand> GetByIdAsync(int id)
    {
        var res = await _repository.GetByIdAsync(id);
        return res;
    }
}

