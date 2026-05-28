using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIINsRepository
{
    Task<UserHIINs> GetUserHIINsAsync(string type);
    Task InsertOrUpdateUserHIINsAsync(string user_id, UserHIINs HIINs, string id);
    Task<UserHIINs> GetSumUserHIINsAsync(string user_id);
}