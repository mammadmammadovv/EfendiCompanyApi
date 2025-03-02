using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;

public interface ISettingRepository : ISettingCommand, ISettingQuery
{

}

public class SettingRepository(ISettingCommand _command, ISettingQuery _query) : ISettingRepository
{
    public async Task PutAsync(Settings model)
    {
        await _command.PutAsync(model);
    }


    public async Task<Settings> GetAsync()
    {
        var res = await _query.GetAsync();
        return res;
    }
}

