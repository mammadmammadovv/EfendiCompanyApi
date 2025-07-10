using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;

public interface ICarModelCommand
{
    Task PostAsync(CarModel model);
    Task PutAsync(CarModel model);
    Task DeleteAsync(int id);
}

public class CarModelCommand(IUnitOfWork _unitOfWork) : ICarModelCommand
{
    public async Task DeleteAsync(int id)
    {
        try
        {
            string _addSql = $@"UPDATE CarModels
                                Set IsDeleted = 1
                                WHERE Id = {id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PostAsync(CarModel model)
    {
        try
        {
            string _addSql = $@"INSERT INTO CarModels(Name,BrandId,StartYear,LogoUrl)
                                VALUES('{model.Name}',
                                '{model.BrandId}',
                                '{model.StartYear}',
                                '{model.LogoUrl}')";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PutAsync(CarModel model)
    {
        try
        {
            string _addSql = $@"UPDATE CarModels
                                Set Name = '{model.Name}',
                                BrandId = '{model.BrandId}',
                                StartYear = '{model.StartYear}',
                                LogoUrl = '{model.LogoUrl}'
                                WHERE Id = {model.Id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

