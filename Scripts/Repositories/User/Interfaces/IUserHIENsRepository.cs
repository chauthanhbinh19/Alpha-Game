using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIENsRepository
{
    Task<UserHIENs> GetUserHIENsAsync(string type);
    Task InsertOrUpdateUserHIENsAsync(string userId, UserHIENs HIENs, string id);
    Task<UserHIENs> GetSumUserHIENsAsync(string userId);
}