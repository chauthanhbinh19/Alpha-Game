using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIINsRepository
{
    Task<UserHIINs> GetUserHIINsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserHIINs>> InsertOrUpdateUserHIINsAsync(string userId, UserHIINs HIINs, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserHIINs> GetSumUserHIINsAsync(string userId);
}