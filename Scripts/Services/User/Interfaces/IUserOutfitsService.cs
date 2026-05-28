using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserOutfitsService
{
    Task<List<Outfits>> GetUserOutfitsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserOutfitsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserOutfitAsync(Outfits outfit, string userId);
    Task<bool> InsertOrUpdateUserOutfitsBatchAsync(List<Outfits> outfits);
    Task<bool> UpdateOutfitLevelAsync(Outfits outfit, int level);
    Task<bool> UpdateOutfitBreakthroughAsync(Outfits outfit, int star, double quantity);
    Task<Outfits> GetUserOutfitByIdAsync(string user_id, string Id);
    Task<Outfits> SumPowerUserOutfitsAsync();
}