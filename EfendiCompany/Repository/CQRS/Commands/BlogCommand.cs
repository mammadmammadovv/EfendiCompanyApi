using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;

public interface IBlogCommand
{
    Task PostAsync(Blog model);
    Task PutAsync(Blog model);
    Task DeleteAsync(int id);
}

public class BlogCommand(IUnitOfWork _unitOfWork) : IBlogCommand
{
    public async Task DeleteAsync(int id)
    {
        try
        {
            string _addSql = $@"UPDATE Blogs
                                Set IsDeleted = 1
                                WHERE Id = {id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PostAsync(Blog model)
    {
        try
        {
            string _addSql = $@"INSERT INTO Blogs(Title,Description,DetailDescription,CreatedBy,ImageUrl)
                                VALUES('{model.Title}',
                                '{model.Description}',
                                '{model.DetailDescription}',
                                '{model.CreatedBy}',
                                '{model.ImageUrl}')";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PutAsync(Blog model)
    {
        try
        {
            string _addSql = $@"UPDATE Blogs
                                Set Title = '{model.Title}',
                                Description = '{model.Description}',
                                DetailDescription = '{model.DetailDescription}',
                                CreatedBy = '{model.CreatedBy}',
                                ImageUrl = '{model.ImageUrl}'
                                WHERE Id = {model.Id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

