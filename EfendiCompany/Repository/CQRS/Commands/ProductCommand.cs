using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;
public interface IProductCommand
{
    Task PostAsync(Product model);
    Task PutAsync(Product model);
    Task DeleteAsync(int id);
}
public class ProductCommand(IUnitOfWork _unitOfWork) : IProductCommand
{
    public async Task DeleteAsync(int id)
    {
        try
        {
            string _addSql = $@"UPDATE Products
                                Set IsDeleted = 1
                                WHERE Id = {id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PostAsync(Product model)
    {
        try
        {
            string _addSql = $@"INSERT INTO Products (Title, Description, ParentProductId, ParentProductName, ImageUrl)
                                VALUES (
                                '{model.Title}',
                                '{model.Description}',
                                '{model.ParentProductId}',
                                CASE 
                                    WHEN '{model.ParentProductId}' = '0' THEN NULL 
                                    ELSE (SELECT Title FROM Products WHERE Id = '{model.ParentProductId}')
                                END,
                                '{model.ImageUrl}'
                                );
                                ";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PutAsync(Product model)
    {
        try
        {
            string _addSql = $@"UPDATE Products
                                Set Title = '{model.Title}',
                                Description = '{model.Description}',
                                ParentProductId = '{model.ParentProductId}',
                                ImageUrl = '{model.ImageUrl}',
                                ParentProductName = CASE 
                                    WHEN '{model.ParentProductId}' = '0' THEN NULL 
                                    ELSE (SELECT Title FROM Products WHERE Id = '{model.ParentProductId}')
                                END
                                WHERE Id = {model.Id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

