using Core.Models;
using Repository.CQRS.Commands;
using Repository.CQRS.Queries;

namespace Repository.Repositories;

public interface ILanguageRepository : ILanguageCommand, ILanguageQuery { }

public class LanguageRepository(ILanguageCommand _command, ILanguageQuery _query) : ILanguageRepository
{
    public async Task PostAsync(Language model)
    {
        await _command.PostAsync(model);
    }

    public async Task PutAsync(Language model)
    {
        await _command.PutAsync(model);
    }

    public async Task DeactivateAsync(int id)
    {
        await _command.DeactivateAsync(id);
    }

    public async Task<IEnumerable<Language>> GetAllAsync()
    {
        return await _query.GetAllAsync();
    }

    public async Task<IEnumerable<Language>> GetActiveAsync()
    {
        return await _query.GetActiveAsync();
    }

    public async Task<Language?> GetByCodeAsync(string code)
    {
        return await _query.GetByCodeAsync(code);
    }

    public async Task<Language?> GetByIdAsync(int id)
    {
        return await _query.GetByIdAsync(id);
    }
}

