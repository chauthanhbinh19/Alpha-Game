using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserRanksService
{ 
    Task<UserRanks> GetUserRanksAsync(string userId, string rankId, IStats stat);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRanksAsync(string userId, UserRanks Ranks, IStats stat, List<RecipeItemDetailDTO> totalDeductions = null);
    Task<UserRanks> GetSumUserRanksAsync(string userId, IStats stat);
}