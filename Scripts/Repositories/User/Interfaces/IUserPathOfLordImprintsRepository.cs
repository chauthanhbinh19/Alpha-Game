using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserPathOfLordImprintsRepository
{
    Task<UserPathOfLordImprints> GetUserPathOfLordImprintsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserPathOfLordImprints>> InsertOrUpdateUserPathOfLordImprintsAsync(string userId, UserPathOfLordImprints PathOfLordImprints, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserPathOfLordImprints> GetSumUserPathOfLordImprintsAsync(string userId);
}