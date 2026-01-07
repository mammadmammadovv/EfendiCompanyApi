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

public class CarModelCommand(IUnitOfWork _unitOfWork, ICarModelImageCommand _imageCommand) : ICarModelCommand
{
    public async Task DeleteAsync(int id)
    {
        try
        {
            // Delete images first
            await _imageCommand.DeleteByCarModelIdAsync(id);
            
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
            string _addSql = $@"INSERT INTO CarModels(Name,BrandId,StartYear,LogoUrl,Condition,FuelType,EnginePower,StartingPrice)
                                VALUES('{model.Name}',
                                '{model.BrandId}',
                                '{model.StartYear}',
                                '{model.LogoUrl}',
                                '{model.Condition}',
                                '{model.FuelType}',
                                '{model.EnginePower}',
                                '{model.StartingPrice}')";

            await _unitOfWork.GetConnection().ExecuteAsync(_addSql, null, _unitOfWork.GetTransaction());
            
            // Get the last inserted ID
            string _getIdSql = @"SELECT last_insert_rowid()";
            var insertedId = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<int>(_getIdSql, null, _unitOfWork.GetTransaction());
            
            // Save images if provided
            if (model.Images != null && model.Images.Any())
            {
                foreach (var image in model.Images)
                {
                    image.CarModelId = insertedId;
                    await _imageCommand.PostAsync(image);
                }
            }
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
                                LogoUrl = '{model.LogoUrl}',
                                Condition = '{model.Condition}',
                                FuelType = '{model.FuelType}',
                                EnginePower = '{model.EnginePower}',
                                StartingPrice = '{model.StartingPrice}'
                                WHERE Id = {model.Id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
            
            // Handle images: delete existing and insert new ones
            if (model.Images != null)
            {
                // Delete existing images
                await _imageCommand.DeleteByCarModelIdAsync(model.Id);
                
                // Insert new images
                if (model.Images.Any())
                {
                    foreach (var image in model.Images)
                    {
                        image.CarModelId = model.Id;
                        await _imageCommand.PostAsync(image);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

