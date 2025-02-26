using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;

public interface IServicesRepository : IServicesCommand, IServicesQuery
{
}
public class ServicesRepository(IServicesQuery _query, IServicesCommand _command) : IServicesRepository
{
    public async Task DeleteAsync(int id)
    {
        await _command.DeleteAsync(id);
    }

    public async Task<IEnumerable<Service>> GetAllAsync()
    {
        var res = await _query.GetAllAsync();
        return res;
    }

    public async Task<Service> GetByIdAsync(int id)
    {
        var res = await _query.GetByIdAsync(id);
        return res;
    }

    public async Task PostAsync(Service model)
    {
        await _command.PostAsync(model);
    }

    public async Task PutAsync(Service model)
    {
        await _command.PutAsync(model);
    }
}
