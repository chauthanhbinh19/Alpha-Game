using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserRanksRepository
{
    Task<UserRanks> GetUserRanksAsync(string userId, string id);
    Task InsertOrUpdateUserRanksAsync(string userId, UserRanks Ranks, string id);
    Task<UserRanks> GetSumUserRanksAsync(string userId);
}