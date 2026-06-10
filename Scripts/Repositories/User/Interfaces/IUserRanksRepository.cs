using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserRanksRepository
{
    Task<UserRanks> GetUserRanksAsync(string type);
    Task InsertOrUpdateUserRanksAsync(string user_id, UserRanks Ranks, string id);
    Task<UserRanks> GetSumUserRanksAsync(string user_id);
}