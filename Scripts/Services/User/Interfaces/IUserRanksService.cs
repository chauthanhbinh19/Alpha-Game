using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserRanksService
{ 
    Task<UserRanks> GetUserRanksAsync(string id);
    Task InsertOrUpdateUserRanksAsync(string user_id, UserRanks Ranks, string id, IStats stat);
    Task<UserRanks> GetSumUserRanksAsync(string user_id);
}