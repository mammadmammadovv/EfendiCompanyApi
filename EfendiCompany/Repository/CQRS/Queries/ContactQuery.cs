using Core.Models;
using Dapper;
using Repository.Infrastructure;

namespace Repository.CQRS.Queries;
public interface IContactQuery
{
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact> GetByIdAsync(int id);
}
public class ContactQuery(IUnitOfWork _unitOfWork) : IContactQuery
{
    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        try
        {
            string _getAllSql = $@"SELECT * FROM Contacts
                                       WHERE IsDeleted = 0
                                       ORDER BY Id Desc";
            var result = await _unitOfWork.GetConnection().QueryAsync<Contact>(_getAllSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Contact> GetByIdAsync(int id)
    {
        try
        {
            string _getByIdSql = $@"SELECT * FROM Contacts
                                       WHERE IsDeleted = 0 AND Id = {id}";

            var result = await _unitOfWork.GetConnection().QueryFirstOrDefaultAsync<Contact>(_getByIdSql, null, _unitOfWork.GetTransaction());
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}