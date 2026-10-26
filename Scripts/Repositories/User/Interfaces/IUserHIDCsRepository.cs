using System.Collections.Generic;
using System.Threading.Tasks;
public interface IUserHIDCsRepository
{
    Task<UserHIDCs> GetUserHIDCsAsync(string userId, string id);
    Task<InsertOrUpdateResult<UserHIDCs>> InsertOrUpdateUserHIDCsAsync(string userId, UserHIDCs HIDCs, string type, List<RecipeItemDetailDTO> totalDeductions);
    Task<UserHIDCs> GetSumUserHIDCsAsync(string userId);
}