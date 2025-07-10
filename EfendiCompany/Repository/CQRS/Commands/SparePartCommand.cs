using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;

public interface ISparePartCommand
{
    Task PostAsync(SparePart model);
    Task PutAsync(SparePart model);
    Task DeleteAsync(int id);
}

public class SparePartCommand(IUnitOfWork _unitOfWork) : ISparePartCommand
{
    public async Task DeleteAsync(int id)
    {
        try
        {
            string _addSql = $@"UPDATE SpareParts
                                Set IsDeleted = 1
                                WHERE Id = {id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PostAsync(SparePart model)
    {
        try
        {
            string _addSql = $@"INSERT INTO SpareParts(Name,ModelId,PartNumber,Price,InStock,ImageUrl)
                                VALUES('{model.Name}',
                                '{model.ModelId}',
                                '{model.PartNumber}',
                                '{model.Price}',
                                '{model.InStock}',
                                '{model.ImageUrl}')";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PutAsync(SparePart model)
    {
        try
        {
            string _addSql = $@"UPDATE SpareParts
                                Set Name = '{model.Name}',
                                ModelId = '{model.ModelId}',
                                PartNumber = '{model.PartNumber}',
                                Price = '{model.Price}',
                                InStock = '{model.InStock}',
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

