using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMagicFormationCirclesService
{
    Task<List<MagicFormationCircles>> GetUserMagicFormationCirclesAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserMagicFormationCirclesCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserMagicFormationCircleAsync(MagicFormationCircles magicFormationCircle, string userId);
    Task<bool> InsertOrUpdateUserMagicFormationCirclesBatchAsync(List<MagicFormationCircles> magicFormationCircles);
    Task<bool> UpdateMagicFormationCircleLevelAsync(MagicFormationCircles magicFormationCircle, int level);
    Task<bool> UpdateMagicFormationCircleBreakthroughAsync(MagicFormationCircles magicFormationCircle, int star, double quantity);
    Task<MagicFormationCircles> GetUserMagicFormationCircleByIdAsync(string user_id, string Id);
    Task<MagicFormationCircles> SumPowerUserMagicFormationCirclesAsync();
}