using Core.Infrastructure;
using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;
public interface ISparePartCategoryRepository : ISparePartCategoryCommand, ISparePartCategoryQuery
{
}
public class SparePartCategoryRepository(ISparePartCategoryCommand _command, ISparePartCategoryQuery _query) : ISparePartCategoryRepository
{
    public async Task PostAsync(SparePartCategory model)
    {
        await _command.PostAsync(model);
    }

    public async Task PutAsync(SparePartCategory model)
    {
        await _command.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _command.DeleteAsync(id);
    }

    public async Task<IEnumerable<SparePartCategory>> GetAllAsync()
    {
        var res = await _query.GetAllAsync();
        return res;
    }

    public async Task<ListResult<SparePartCategory>> GetPaginationAsync(int offset, int limit)
    {
        var res = await _query.GetPaginationAsync(offset, limit);
        return res;
    }

    public Task<SparePartCategory> GetByIdAsync(int id)
    {
        return _query.GetByIdAsync(id);
    }
}

