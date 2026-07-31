using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMagicFormationCirclesRepository
{
    Task<List<MagicFormationCircles>> GetUserMagicFormationCirclesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserMagicFormationCirclesCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<MagicFormationCircles>> InsertOrUpdateUserMagicFormationCircleAsync(string userId, MagicFormationCircles magicFormationCirlce);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<MagicFormationCircles>>> InsertOrUpdateUserMagicFormationCirclesBatchAsync(string userId, List<MagicFormationCircles> magicFormationCirlces);
    Task<InsertOrUpdateResult<bool>> UpdateUserMagicFormationCircleLevelAsync(string userId, MagicFormationCircles magicFormationCirlce);
    Task<InsertOrUpdateResult<bool>> UpdateUserMagicFormationCircleStarAsync(string userId, MagicFormationCircles magicFormationCirlce);
    Task<MagicFormationCircles> GetUserMagicFormationCircleByIdAsync(string userId, string Id);
    Task<MagicFormationCircles> SumPowerUserMagicFormationCirclesAsync(string userId);
}