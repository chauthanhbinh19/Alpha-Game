using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserOutfitsRepository
{
    Task<List<Outfits>> GetUserOutfitsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserOutfitsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<Outfits>> InsertOrUpdateUserOutfitAsync(string userId, Outfits outfit);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Outfits>>> InsertOrUpdateUserOutfitsBatchAsync(string userId, List<Outfits> outfits);
    Task<InsertOrUpdateResult<bool>> UpdateUserOutfitLevelAsync(string userId, Outfits outfit);
    Task<InsertOrUpdateResult<bool>> UpdateUserOutfitStarAsync(string userId, Outfits outfit);
    Task<Outfits> GetUserOutfitByIdAsync(string userId, string Id);
    Task<Outfits> SumPowerUserOutfitsAsync(string userId);
}