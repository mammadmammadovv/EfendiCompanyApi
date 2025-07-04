using Core.Infrastructure;
using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;
public interface ICarModelRepository : ICarModelCommand, ICarModelQuery
{
}
public class CarModelRepository(ICarModelCommand _command, ICarModelQuery _query) : ICarModelRepository
{
    public async Task PostAsync(CarModel model)
    {
        await _command.PostAsync(model);
    }

    public async Task PutAsync(CarModel model)
    {
        await _command.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _command.DeleteAsync(id);
    }

    public async Task<IEnumerable<CarModel>> GetAllAsync()
    {
        var res = await _query.GetAllAsync();
        return res;
    }

    public async Task<ListResult<CarModel>> GetPaginationAsync(int offset, int limit)
    {
        var res = await _query.GetPaginationAsync(offset, limit);
        return res;
    }

    public Task<CarModel> GetByIdAsync(int id)
    {
        return _query.GetByIdAsync(id);
    }
}

