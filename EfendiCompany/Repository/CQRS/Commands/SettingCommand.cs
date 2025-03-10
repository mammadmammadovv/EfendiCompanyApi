﻿using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;

public interface ISettingCommand
{
    Task PutAsync(Settings model);
}
public class SettingCommand(IUnitOfWork _unitOfWork) : ISettingCommand
{
    public async Task PutAsync(Settings model)
    {
        try
        {
            string _addSql = $@"UPDATE Settings
                                Set Address = '{model.Address}',
                                Email = '{model.Email}',
                                PhoneNumber = '{model.PhoneNumber}',
                                Fax = '{model.Fax}',
                                ZipCode = '{model.ZipCode}',
                                LogoUrl = '{model.LogoUrl}',
                                XUrl = '{model.XUrl}',
                                FacebookUrl = '{model.FacebookUrl}',
                                InstagramUrl = '{model.InstagramUrl}',
                                PinterestUrl = '{model.PinterestUrl}',
                                YouTubeUrl = '{model.YouTubeUrl}',
                                LinkedinUrl = '{model.LinkedinUrl}'";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

