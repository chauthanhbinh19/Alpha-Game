using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIINsService
{ 
    Task<UserHIINs> GetUserHIINsAsync(string userId, string id);
    Task InsertOrUpdateUserHIINsAsync(string userId, UserHIINs HIINs, string id);
    Task<UserHIINs> GetSumUserHIINsAsync(string userId);
}