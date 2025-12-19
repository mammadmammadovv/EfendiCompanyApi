using Core.Exceptions;
using Core.Models;
using Repository.Repositories;

namespace Services.Services;

public interface ILanguageService
{
    Task PostAsync(Language model);
    Task PutAsync(Language model);
    Task DeactivateAsync(int id);
    Task<IEnumerable<Language>> GetAllAsync();
    Task<IEnumerable<Language>> GetActiveAsync();
    Task<Language?> GetByCodeAsync(string code);
    Task<Language?> GetByIdAsync(int id);
}

public class LanguageService(ILanguageRepository _repository) : ILanguageService
{
    public async Task PostAsync(Language model)
    {
        await _repository.PostAsync(model);
    }

    public async Task PutAsync(Language model)
    {
        await _repository.PutAsync(model);
    }

    public async Task DeactivateAsync(int id)
    {
        await _repository.DeactivateAsync(id);
    }

    public async Task<IEnumerable<Language>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<Language>> GetActiveAsync()
    {
        return await _repository.GetActiveAsync();
    }

    public async Task<Language?> GetByCodeAsync(string code)
    {
        return await _repository.GetByCodeAsync(code);
    }

    public async Task<Language?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
