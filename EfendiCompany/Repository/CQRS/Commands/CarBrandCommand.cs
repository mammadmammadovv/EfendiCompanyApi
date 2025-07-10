using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;

public interface ICarBrandCommand
{
    Task PostAsync(CarBrand model);
    Task PutAsync(CarBrand model);
    Task DeleteAsync(int id);
}

public class CarBrandCommand(IUnitOfWork _unitOfWork) : ICarBrandCommand
{
    public async Task DeleteAsync(int id)
    {
        try
        {
            string _addSql = $@"UPDATE CarBrands
                                Set IsDeleted = 1
                                WHERE Id = {id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PostAsync(CarBrand model)
    {
        try
        {
            string _addSql = $@"INSERT INTO CarBrands(Name,Country,LogoUrl)
                                VALUES('{model.Name}',
                                '{model.Country}',
                                '{model.LogoUrl}')";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PutAsync(CarBrand model)
    {
        try
        {
            string _addSql = $@"UPDATE CarBrands
                                Set Name = '{model.Name}',
                                Country = '{model.Country}',
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

