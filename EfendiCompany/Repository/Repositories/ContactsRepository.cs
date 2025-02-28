using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;

public interface IContactsRepository : IContactCommand, IContactQuery
{
}
public class ContactsRepository(IContactQuery _query, IContactCommand _command) : IContactsRepository
{
    public async Task DeleteAsync(int id)
    {
        await _command.DeleteAsync(id);
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        var res = await _query.GetAllAsync();
        return res;
    }

    public async Task<Contact> GetByIdAsync(int id)
    {
        var res = await _query.GetByIdAsync(id);
        return res;
    }

    public async Task PostAsync(Contact model)
    {
        await _command.PostAsync(model);
    }

    public async Task PutAsync(Contact model)
    {
        await _command.PutAsync(model);
    }
}