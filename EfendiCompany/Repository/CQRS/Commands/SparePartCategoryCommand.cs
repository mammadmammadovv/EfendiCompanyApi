using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;

public interface ISparePartCategoryCommand
{
    Task PostAsync(SparePartCategory model);
    Task PutAsync(SparePartCategory model);
    Task DeleteAsync(int id);
}

public class SparePartCategoryCommand(IUnitOfWork _unitOfWork) : ISparePartCategoryCommand
{
    public async Task DeleteAsync(int id)
    {
        try
        {
            string _addSql = $@"UPDATE SparePartCategories
                                Set IsDeleted = 1
                                WHERE Id = {id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PostAsync(SparePartCategory model)
    {
        try
        {
            string _addSql = $@"INSERT INTO SparePartCategories(Name)
                                VALUES('{model.Name}')";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PutAsync(SparePartCategory model)
    {
        try
        {
            string _addSql = $@"UPDATE SparePartCategories
                                Set Name = '{model.Name}'
                                WHERE Id = {model.Id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

