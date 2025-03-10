using Core.Infrastructure;
using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;

public interface IAboutUsRepository : IAboutUsCommand, IAboutUsQuery { }
public class AboutUsRepository(IAboutUsCommand _command, IAboutUsQuery _query) : IAboutUsRepository
{
    public async Task PostAsync(AboutUs model)
    {
        await _command.PostAsync(model);
    }

    public async Task PutAsync(AboutUs model)
    {
        await _command.PutAsync(model);
    }

    public async Task DeleteAsync(int id)
    {
        await _command.DeleteAsync(id);
    }

    public async Task<IEnumerable<AboutUs>> GetAllAsync()
    {
        var res = await _query.GetAllAsync();
        return res;
    }

    public async Task<ListResult<AboutUs>> GetPaginationAsync(int offset, int limit)
    {
        var res = await _query.GetPaginationAsync(offset, limit);
        return res;
    }


    public async Task<AboutUs> GetByIdAsync(int id)
    {
        var res = await _query.GetByIdAsync(id);
        return res;
    }
}

