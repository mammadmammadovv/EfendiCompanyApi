using Core.Infrastructure;
using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;
public interface ICarBrandRepository : ICarBrandCommand, ICarBrandQuery
{
}
public class CarBrandRepository(ICarBrandCommand _command, ICarBrandQuery _query) : ICarBrandRepository
{
    public async Task PostAsync(CarBrand model)
    {
        await _command.PostAsync(model);
    }

    public async Task PutAsync(CarBrand model)
    {
        await _command.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _command.DeleteAsync(id);
    }

    public async Task<IEnumerable<CarBrand>> GetAllAsync()
    {
        var res = await _query.GetAllAsync();
        return res;
    }

    public async Task<ListResult<CarBrand>> GetPaginationAsync(int offset, int limit)
    {
        var res = await _query.GetPaginationAsync(offset, limit);
        return res;
    }

    public Task<CarBrand> GetByIdAsync(int id)
    {
        return _query.GetByIdAsync(id);
    }
}

