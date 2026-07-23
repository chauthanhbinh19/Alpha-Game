using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMagicFormationCirclesRepository
{
    Task<List<MagicFormationCircles>> GetUserMagicFormationCirclesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserMagicFormationCirclesCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserMagicFormationCircleAsync(MagicFormationCircles magicFormationCircle, string userId);
    Task<bool> InsertOrUpdateUserMagicFormationCirclesBatchAsync(string userId, List<MagicFormationCircles> magicFormationCircles);
    Task<bool> UpdateUserMagicFormationCircleLevelAsync(string userId, MagicFormationCircles magicFormationCircle);
    Task<bool> UpdateUserMagicFormationCircleStarAsync(string userId, MagicFormationCircles magicFormationCircle);
    Task<bool> UpdateUserMagicFormationCircleBreakthroughAsync(string userId, MagicFormationCircles magicFormationCircle, int star, double quantity);
    Task<MagicFormationCircles> GetUserMagicFormationCircleByIdAsync(string userId, string Id);
    Task<MagicFormationCircles> SumPowerUserMagicFormationCirclesAsync(string userId);
}