using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserRanksRepository
{
    Task<UserRanks> GetUserRanksAsync(string userId, string rankId, string objectId, string userTable, string objectColumn);
    Task<InsertOrUpdateResult<UserRanks>> InsertOrUpdateUserRanksAsync(string userId, UserRanks userRank, string objectId, string userTable, string objectColumn, List<RecipeItemDetailDTO> totalDeductions = null);
    Task<UserRanks> GetSumUserRanksAsync(string userId, string objectId, string userTable, string objectColumn);
}