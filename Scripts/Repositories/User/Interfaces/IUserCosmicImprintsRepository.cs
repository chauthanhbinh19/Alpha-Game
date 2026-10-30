using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserCosmicImprintsRepository
{
    Task<UserCosmicImprints> GetUserCosmicImprintsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserCosmicImprints>> InsertOrUpdateUserCosmicImprintsAsync(string userId, UserCosmicImprints CosmicImprints, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserCosmicImprints> GetSumUserCosmicImprintsAsync(string userId);
}