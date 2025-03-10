using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;
public interface IHomeRepository : IHomeQuery, IHomeCommand
{

}
public class HomeRepository(IHomeQuery _query, IHomeCommand _command) : IHomeRepository
{
    public async Task<Home> GetAsync()
    {
        var res = await _query.GetAsync();
        return res;
    }

    public async Task PutAsync(Home model)
    {
        await _command.PutAsync(model);
    }
}

