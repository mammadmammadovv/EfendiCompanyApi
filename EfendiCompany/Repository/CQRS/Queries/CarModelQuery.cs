using Core.Infrastructure;
using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;

public interface ICarModelQuery
{
    Task<IEnumerable<CarModel>> GetAllAsync();
    Task<ListResult<CarModel>> GetPaginationAsync(int offset, int limit);
    Task<CarModel> GetByIdAsync(int id);
}
public class CarModelQuery(IUnitOfWork _unitOfWork, ICarModelImageQuery _imageQuery) : ICarModelQuery
{
    public async Task<IEnumerable<CarModel>> GetAllAsync()
    {
        try
        {
            string _getAllSql = $@"SELECT Id, Name, BrandId, 
                                   CASE 
                                       WHEN StartYear IS NULL OR StartYear = '' THEN NULL
                                       WHEN StartYear LIKE '%.%.%' THEN 
                                           substr(StartYear, 7, 4) || '-' || 
                                           substr('0' || substr(StartYear, 4, 2), -2) || '-' || 
                                           substr('0' || substr(StartYear, 1, 2), -2) || 
                                           CASE WHEN length(StartYear) > 10 THEN ' ' || substr(StartYear, 12) ELSE '' END
                                       ELSE StartYear
                                   END as StartYear,
                                   LogoUrl, Condition, FuelType, EnginePower, StartingPrice, IsDeleted,
                                   CASE 
                                       WHEN CreatedDate IS NULL OR CreatedDate = '' THEN datetime('now')
                                       WHEN CreatedDate LIKE '%.%.%' THEN 
                                           substr(CreatedDate, 7, 4) || '-' || 
                                           substr('0' || substr(CreatedDate, 4, 2), -2) || '-' || 
                                           substr('0' || substr(CreatedDate, 1, 2), -2) || 
                                           CASE WHEN length(CreatedDate) > 10 THEN ' ' || substr(CreatedDate, 12) ELSE '' END
                                       ELSE CreatedDate
                                   END as CreatedDate
                                   FROM CarModels
                                   WHERE IsDeleted = 0
                                   ORDER BY Id Desc";
            var result = await _unitOfWork.GetConnection().QueryAsync<CarModel>(_getAllSql, null, _unitOfWork.GetTransaction());
            
            // Load images for each car model
            foreach (var carModel in result)
            {
                carModel.Images = (await _imageQuery.GetByCarModelIdAsync(carModel.Id)).ToList();
            }
            
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ListResult<CarModel>> GetPaginationAsync(int offset, int limit)
    {
        try
        {
            string _getAllSql = $@"SELECT Id, Name, BrandId, 
                                   CASE 
                                       WHEN StartYear IS NULL OR StartYear = '' THEN NULL
                                       WHEN StartYear LIKE '%.%.%' THEN 
                                           substr(StartYear, 7, 4) || '-' || 
                                           substr('0' || substr(StartYear, 4, 2), -2) || '-' || 
                                           substr('0' || substr(StartYear, 1, 2), -2) || 
                                           CASE WHEN length(StartYear) > 10 THEN ' ' || substr(StartYear, 12) ELSE '' END
                                       ELSE StartYear
                                   END as StartYear,
                                   LogoUrl, Condition, FuelType, EnginePower, StartingPrice, IsDeleted,
                                   CASE 
                                       WHEN CreatedDate IS NULL OR CreatedDate = '' THEN datetime('now')
                                       WHEN CreatedDate LIKE '%.%.%' THEN 
                                           substr(CreatedDate, 7, 4) || '-' || 
                                           substr('0' || substr(CreatedDate, 4, 2), -2) || '-' || 
                                           substr('0' || substr(CreatedDate, 1, 2), -2) || 
                                           CASE WHEN length(CreatedDate) > 10 THEN ' ' || substr(CreatedDate, 12) ELSE '' END
                                       ELSE CreatedDate
                                   END as CreatedDate
                                   FROM CarModels
                                   WHERE IsDeleted = 0
                                   ORDER BY Id Desc
                                   LIMIT @Limit OFFSET @Offset;

                                   SELECT COUNT(*) TotalCount FROM CarModels WHERE IsDeleted = 0";
            using (var multi = await _unitOfWork.GetConnection().QueryMultipleAsync(_getAllSql, new { Offset = offset, Limit = limit }, _unitOfWork.GetTransaction()))
            {
                var aboutUsList = (await multi.ReadAsync<CarModel>()).ToList();
                var totalCount = await multi.ReadFirstAsync<int>();

                // Load images for each car model
                foreach (var carModel in aboutUsList)
                {
                    carModel.Images = (await _imageQuery.GetByCarModelIdAsync(carModel.Id)).ToList();
                }

                var result = new ListResult<CarModel>
                {
                    Data = aboutUsList,
                    TotalCount = totalCount
                };

                return result;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CarModel> GetByIdAsync(int id)
    {
        try
        {
            string _getByIdSql = $@"SELECT Id, Name, BrandId, 
                                   CASE 
                                       WHEN StartYear IS NULL OR StartYear = '' THEN NULL
                                       WHEN StartYear LIKE '%.%.%' THEN 
                                           substr(StartYear, 7, 4) || '-' || 
                                           substr('0' || substr(StartYear, 4, 2), -2) || '-' || 
                                           substr('0' || substr(StartYear, 1, 2), -2) || 
                                           CASE WHEN length(StartYear) > 10 THEN ' ' || substr(StartYear, 12) ELSE '' END
                                       ELSE StartYear
                                   END as StartYear,
                                   LogoUrl, Condition, FuelType, EnginePower, StartingPrice, IsDeleted,
                                   CASE 
                                       WHEN CreatedDate IS NULL OR CreatedDate = '' THEN datetime('now')
                                       WHEN CreatedDate LIKE '%.%.%' THEN 
                                           substr(CreatedDate, 7, 4) || '-' || 
                                           substr('0' || substr(CreatedDate, 4, 2), -2) || '-' || 
                                           substr('0' || substr(CreatedDate, 1, 2), -2) || 
                                           CASE WHEN length(CreatedDate) > 10 THEN ' ' || substr(CreatedDate, 12) ELSE '' END
                                       ELSE CreatedDate
                                   END as CreatedDate
                                   FROM CarModels
                                   WHERE IsDeleted = 0 AND Id = {id}";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<CarModel>(_getByIdSql, null, _unitOfWork.GetTransaction());
            
            if (result != null)
            {
                // Load images for the car model
                result.Images = (await _imageQuery.GetByCarModelIdAsync(result.Id)).ToList();
            }
            
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}

