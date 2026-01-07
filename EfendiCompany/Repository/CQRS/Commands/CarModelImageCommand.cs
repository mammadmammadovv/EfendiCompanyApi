using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;

public interface ICarModelImageCommand
{
    Task PostAsync(CarModelImage image);
    Task PostMultipleAsync(List<CarModelImage> images);
    Task PutAsync(CarModelImage image);
    Task DeleteAsync(int id);
    Task DeleteByCarModelIdAsync(int carModelId);
}

public class CarModelImageCommand(IUnitOfWork _unitOfWork) : ICarModelImageCommand
{
    public async Task DeleteAsync(int id)
    {
        try
        {
            string _deleteSql = $@"UPDATE CarModelImages
                                Set IsDeleted = 1
                                WHERE Id = {id}";

            await _unitOfWork.GetConnection().QueryAsync(_deleteSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task DeleteByCarModelIdAsync(int carModelId)
    {
        try
        {
            string _deleteSql = $@"UPDATE CarModelImages
                                Set IsDeleted = 1
                                WHERE CarModelId = {carModelId}";

            await _unitOfWork.GetConnection().QueryAsync(_deleteSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PostAsync(CarModelImage image)
    {
        try
        {
            string _addSql = $@"INSERT INTO CarModelImages(CarModelId,ImageUrl,DisplayOrder)
                                VALUES({image.CarModelId},
                                '{image.ImageUrl}',
                                {image.DisplayOrder ?? 0})";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PostMultipleAsync(List<CarModelImage> images)
    {
        try
        {
            if (images == null || images.Count == 0)
                return;

            foreach (var image in images)
            {
                await PostAsync(image);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PutAsync(CarModelImage image)
    {
        try
        {
            string _updateSql = $@"UPDATE CarModelImages
                                Set CarModelId = {image.CarModelId},
                                ImageUrl = '{image.ImageUrl}',
                                DisplayOrder = {image.DisplayOrder ?? 0}
                                WHERE Id = {image.Id}";

            await _unitOfWork.GetConnection().QueryAsync(_updateSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

