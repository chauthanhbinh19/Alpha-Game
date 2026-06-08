using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIINsRepository
{
    Task<UserHIINs> GetUserHIINsAsync(string type);
    Task InsertOrUpdateUserHIINsAsync(string userId, UserHIINs HIINs, string id);
    Task<UserHIINs> GetSumUserHIINsAsync(string userId);
}