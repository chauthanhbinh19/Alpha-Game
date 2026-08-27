using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserRanksRepository
{
    Task<UserRanks> GetUserRanksAsync(string userId, string rankId, string objectId, string userTable, string objectColumn);
    Task InsertOrUpdateUserRanksAsync(string userId, UserRanks Ranks, string objectId, string userTable, string objectColumn);
    Task<UserRanks> GetSumUserRanksAsync(string userId, string objectId, string userTable, string objectColumn);
}