using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserPrimordialImprintsService
{ 
    Task<UserPrimordialImprints> GetUserPrimordialImprintsAsync(string userId, string id);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPrimordialImprintsAsync(string userId, UserPrimordialImprints PrimordialImprints, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserPrimordialImprints> GetSumUserPrimordialImprintsAsync(string userId);
}