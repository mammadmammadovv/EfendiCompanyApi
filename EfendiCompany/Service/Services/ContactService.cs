using Core.Models;
using Repository.Repositories;

namespace Services.Services;

public interface IContactService
{
    Task PostAsync(Contact model);
    Task PutAsync(Contact model);
    Task DeleteAsync(int id);
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact> GetByIdAsync(int id);
}

public class ContactService(IContactsRepository _repo) : IContactService
{
    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        var res = await _repo.GetAllAsync();
        return res;
    }

    public async Task<Contact> GetByIdAsync(int id)
    {
        var res = await _repo.GetByIdAsync(id);
        return res;
    }

    public async Task PostAsync(Contact model)
    {
        await _repo.PostAsync(model);
    }

    public async Task PutAsync(Contact model)
    {
        await _repo.PutAsync(model);
    }
}
