using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHICAsRepository
{
    Task<UserHICAs> GetUserHICAsAsync(string type);
    Task InsertOrUpdateUserHICAsAsync(string user_id, UserHICAs HICAs, string id);
    Task<UserHICAs> GetSumUserHICAsAsync(string user_id);
}