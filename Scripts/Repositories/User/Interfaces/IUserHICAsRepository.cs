using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHICAsRepository
{
    Task<UserHICAs> GetUserHICAsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserHICAs>> InsertOrUpdateUserHICAsAsync(string userId, UserHICAs HICAs, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserHICAs> GetSumUserHICAsAsync(string userId);
}