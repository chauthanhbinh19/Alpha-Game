using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserRanksService
{ 
    Task<UserRanks> GetUserRanksAsync(string userId, string rankId, IStats stat);
    Task InsertOrUpdateUserRanksAsync(string userId, UserRanks Ranks, IStats stat);
    Task<UserRanks> GetSumUserRanksAsync(string userId, IStats stat);
}