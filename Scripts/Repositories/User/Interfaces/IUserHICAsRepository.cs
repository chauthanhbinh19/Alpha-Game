using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHICAsRepository
{
    Task<UserHICAs> GetUserHICAsAsync(string type);
    Task InsertOrUpdateUserHICAsAsync(string userId, UserHICAs HICAs, string id);
    Task<UserHICAs> GetSumUserHICAsAsync(string userId);
}