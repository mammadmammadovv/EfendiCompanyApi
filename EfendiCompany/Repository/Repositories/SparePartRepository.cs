using Core.Infrastructure;
using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;
public interface ISparePartRepository : ISparePartCommand, ISparePartQuery
{
}
public class SparePartRepository(ISparePartCommand _command, ISparePartQuery _query) : ISparePartRepository
{
    public async Task PostAsync(SparePart model)
    {
        await _command.PostAsync(model);
    }

    public async Task PutAsync(SparePart model)
    {
        await _command.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _command.DeleteAsync(id);
    }

    public async Task<IEnumerable<SparePart>> GetAllAsync()
    {
        var res = await _query.GetAllAsync();
        return res;
    }

    public async Task<ListResult<SparePart>> GetPaginationAsync(int offset, int limit)
    {
        var res = await _query.GetPaginationAsync(offset, limit);
        return res;
    }

    public Task<SparePart> GetByIdAsync(int id)
    {
        return _query.GetByIdAsync(id);
    }
}

