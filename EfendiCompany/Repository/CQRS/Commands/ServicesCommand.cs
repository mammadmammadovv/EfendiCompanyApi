using Core.Models;
using Dapper;
using Repository.Infrastructure;
using System.Reflection;

namespace Repository.CQRS.Commands;

public interface IServicesCommand
{
    Task PostAsync(Service model);
    Task PutAsync(Service model);
    Task DeleteAsync(int id);
}
public class ServicesCommand(IUnitOfWork _unitOfWork) : IServicesCommand
{
    public async Task DeleteAsync(int id)
    {
        try
        {
            string _addSql = $@"UPDATE Services
                                Set IsDeleted = 1
                                WHERE Id = {id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PostAsync(Service model)
    {
        try
        {
            string _addSql = $@"INSERT INTO Services(Title,Description,FullDescription,ImageUrl,ImageDetailsUrl)
                                VALUES('{model.Title}',
                                '{model.Description}',
                                '{model.FullDescription}',
                                '{model.ImageUrl}',
                                '{model.ImageDetailsUrl}')";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PutAsync(Service model)
    {
        try
        {
            string _addSql = $@"UPDATE Services
                                Set Title = '{model.Title}',
                                Description = '{model.Description}',
                                FullDescription = '{model.FullDescription}',
                                ImageUrl = '{model.ImageUrl}',
                                ImageDetailsUrl = '{model.ImageDetailsUrl}'
                                WHERE Id = {model.Id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
