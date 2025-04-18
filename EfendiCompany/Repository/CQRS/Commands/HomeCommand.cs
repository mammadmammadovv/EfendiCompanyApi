﻿using Core.Models;
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
            string sql = @"
            UPDATE Home
            SET 
                SliderList = @SliderList,
                AdvantagesList = @AdvantagesList,
                PackagesList = @PackagesList,
                ServicesList = @ServicesList,
                StatisticsList = @StatisticsList,
                PricesList = @PricesList,
                Sign = @Sign";

            var parameters = new
            {
                model.SliderList,
                model.AdvantagesList,
                model.PackagesList,
                model.ServicesList,
                model.StatisticsList,
                model.PricesList,
                model.Sign
            };

            await _unitOfWork
                .GetConnection()
                .ExecuteAsync(sql, parameters, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            // Loglama əlavə edə bilərsən buraya
            throw; // İstəyirsənsə, throw ex; yox, sadəcə throw saxlamaq daha doğrudur (stack trace qorunur)
        }
    }
}

