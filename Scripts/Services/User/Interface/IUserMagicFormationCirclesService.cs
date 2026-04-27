using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMagicFormationCirclesService
{
    Task<List<MagicFormationCircles>> GetUserMagicFormationCirclesAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserMagicFormationCirclesCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserMagicFormationCircleAsync(MagicFormationCircles MagicFormationCircle, string userId);
    Task<bool> UpdateMagicFormationCircleLevelAsync(MagicFormationCircles MagicFormationCircle, int cardLevel);
    Task<bool> UpdateMagicFormationCircleBreakthroughAsync(MagicFormationCircles MagicFormationCircle, int star, double quantity);
    Task<MagicFormationCircles> GetUserMagicFormationCircleByIdAsync(string user_id, string Id);
    Task<MagicFormationCircles> SumPowerUserMagicFormationCirclesAsync();
}