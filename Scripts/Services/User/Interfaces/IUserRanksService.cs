using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserRanksService
{ 
    Task<UserRanks> GetUserRanksAsync(string userId, string id);
    Task InsertOrUpdateUserRanksAsync(string userId, UserRanks Ranks, string id, IStats stat);
    Task<UserRanks> GetSumUserRanksAsync(string userId);
}