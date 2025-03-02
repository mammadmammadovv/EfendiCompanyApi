using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Commands;

public interface IAboutUsCommand
{
    Task PostAsync(AboutUs model);
    Task PutAsync(AboutUs model);
    Task DeleteAsync(int id);
}

public class AboutUsCommand(IUnitOfWork _unitOfWork) : IAboutUsCommand
{
    public async Task PostAsync(AboutUs model)
    {
        try
        {
            string _addSql = $@"INSERT INTO AboutUs(Title,Description,MissionTitle,MissionDescription,MissionImageUrl,VisionTitle,VisionDescription,VisionImageUrl,WhyUsTitle,WhyUsDescription,WhyUsImageUrl,TeamMembers)
                                VALUES('{model.Title}',
                                '{model.Description}',
                                '{model.MissionTitle}',
                                '{model.MissionDescription}',
                                '{model.MissionImageUrl}',
                                '{model.VisionTitle}',
                                '{model.VisionDescription}',
                                '{model.VisionImageUrl}',
                                '{model.WhyUsTitle}',
                                '{model.WhyUsDescription}',
                                '{model.WhyUsImageUrl}',
                                '{model.TeamMembers}')";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task PutAsync(AboutUs model)
    {
        try
        {
            string _addSql = $@"UPDATE AboutUs
                                Set Title = '{model.Title}',
                                Description = '{model.Description}',
                                MissionTitle = '{model.MissionTitle}',
                                MissionDescription = '{model.MissionDescription}',
                                MissionImageUrl = '{model.MissionImageUrl}',
                                VisionTitle = '{model.VisionTitle}',
                                VisionDescription = '{model.VisionDescription}',
                                VisionImageUrl = '{model.VisionImageUrl}',
                                WhyUsTitle = '{model.WhyUsTitle}',
                                WhyUsDescription = '{model.WhyUsDescription}',
                                WhyUsImageUrl = '{model.WhyUsImageUrl}',
                                TeamMembers = '{model.TeamMembers}'
                                WHERE Id = {model.Id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            string _addSql = $@"UPDATE AboutUs
                                Set IsDeleted = 1
                                WHERE Id = {id}";

            await _unitOfWork.GetConnection().QueryAsync(_addSql, null, _unitOfWork.GetTransaction());
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
