using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMagicFormationCirclesService
{
    Task<List<MagicFormationCircles>> GetUserMagicFormationCirclesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserMagicFormationCirclesCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMagicFormationCircleAsync(string userId, MagicFormationCircles magicFormationCircle);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMagicFormationCirclesBatchAsync(string userId, List<MagicFormationCircles> magicFormationCircles);
    Task<bool> UpdateUserMagicFormationCircleLevelAsync(string userId, MagicFormationCircles magicFormationCircle);
    Task<bool> UpdateUserMagicFormationCircleStarAsync(string userId, MagicFormationCircles magicFormationCircle);
    Task<MagicFormationCircles> GetUserMagicFormationCircleByIdAsync(string userId, string Id);
    Task<MagicFormationCircles> SumPowerUserMagicFormationCirclesAsync(string userId);
}