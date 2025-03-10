using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;

public interface IHomeCommand
{
    Task PutAsync(Home model);
}
public class HomeCommand(IUnitOfWork _unitOfWork) : IHomeCommand
{
    public async Task PutAsync(Home model)
    {
        try
        {
            string sql = $@"UPDATE Home
                                Set SliderList = '{model.SliderList}',
                                AdvantagesList = '{model.AdvantagesList}',
                                PackagesList = '{model.PackagesList}',
                                ServicesList = '{model.ServicesList}'
                                ";

            await _unitOfWork.GetConnection().QueryAsync(sql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

