using Core.Infrastructure;
using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;
public interface IBlogRepository : IBlogCommand, IBlogQuery
{
}
public class BlogRepository(IBlogCommand _command, IBlogQuery _query) : IBlogRepository
{
    public async Task PostAsync(Blog model)
    {
        await _command.PostAsync(model);
    }

    public async Task PutAsync(Blog model)
    {
        await _command.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _command.DeleteAsync(id);
    }

    public async Task<IEnumerable<Blog>> GetAllAsync()
    {
        var res = await _query.GetAllAsync();
        return res;
    }

    public async Task<ListResult<Blog>> GetPaginationAsync(int offset, int limit)
    {
        var res = await _query.GetPaginationAsync(offset, limit);
        return res;
    }

    public Task<Blog> GetByIdAsync(int id)
    {
        return _query.GetByIdAsync(id);
    }
}

