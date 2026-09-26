using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIINsRepository
{
    Task<UserHIINs> GetUserHIINsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserHIINs>> InsertOrUpdateUserHIINsAsync(string userId, UserHIINs HIINs, string id);
    Task<UserHIINs> GetSumUserHIINsAsync(string userId);
}