using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserPrimordialImprintsRepository
{
    Task<UserPrimordialImprints> GetUserPrimordialImprintsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserPrimordialImprints>> InsertOrUpdateUserPrimordialImprintsAsync(string userId, UserPrimordialImprints PrimordialImprints, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserPrimordialImprints> GetSumUserPrimordialImprintsAsync(string userId);
}