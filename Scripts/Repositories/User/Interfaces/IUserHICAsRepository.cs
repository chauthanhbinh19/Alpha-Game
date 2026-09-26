using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHICAsRepository
{
    Task<UserHICAs> GetUserHICAsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserHICAs>> InsertOrUpdateUserHICAsAsync(string userId, UserHICAs HICAs, string id);
    Task<UserHICAs> GetSumUserHICAsAsync(string userId);
}