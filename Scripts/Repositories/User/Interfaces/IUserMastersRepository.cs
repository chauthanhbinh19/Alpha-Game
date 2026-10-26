using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserMastersRepository
{
    Task<UserMasters> GetUserMastersAsync(string userId, string masterId, string objectId, string userTable, string objectColumn);
    Task<InsertOrUpdateResult<UserMasters>> InsertOrUpdateUserMastersAsync(string userId, UserMasters userMaster, string objectId, string userTable, string objectColumn, List<RecipeItemDetailDTO> totalDeductions = null);
    Task<UserMasters> GetSumUserMastersAsync(string userId, string objectId, string userTable, string objectColumn);
}