using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;

public interface IContactCommand
{
    Task PostAsync(Contact model);
    Task PutAsync(Contact model);
    Task DeleteAsync(int id);
}
public class ContactCommand(IUnitOfWork _unitOfWork) : IContactCommand
{
    public async Task DeleteAsync(int id)
    {
        try
        {
            string _addSql = $@"UPDATE Contacts
                                Set IsDeleted = 1
                                WHERE Id = {id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PostAsync(Contact model)
    {
        try
        {
            string _addSql = $@"INSERT INTO Contacts(Title,Description,Address,Email,FormTitle,PhoneNumber)
                                VALUES('{model.Title}',
                                '{model.Description}',
                                '{model.Address}',
                                '{model.Email}',
                                '{model.FormTitle}',
                                '{model.PhoneNumber}')";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PutAsync(Contact model)
    {
        try
        {
            string _addSql = $@"UPDATE Contacts
                                Set Title = '{model.Title}',
                                Description = '{model.Description}',
                                Address = '{model.Address}',
                                Email = '{model.Email}',
                                PhoneNumber = '{model.PhoneNumber}',
                                FormTitle = '{model.FormTitle}'";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
