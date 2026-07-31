using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSpiritBeastsRepository
{
    Task<List<SpiritBeasts>> GetUserSpiritBeastsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<List<SpiritBeasts>> GetAllUserSpiritBeastsAsync(string userId, int pageSize, int offset);
    Task<List<SpiritBeasts>> GetUserSpiritBeastsByCardIdsAsync(string userId, List<string> cardIds);
    Task<int> GetUserSpiritBeastsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<SpiritBeasts>> InsertOrUpdateUserSpiritBeastAsync(string userId, SpiritBeasts spiritBeast);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<SpiritBeasts>>> InsertOrUpdateUserSpiritBeastsBatchAsync(string userId, List<SpiritBeasts> spiritBeasts);
    Task<InsertOrUpdateResult<bool>> UpdateUserSpiritBeastLevelAsync(string userId, SpiritBeasts spiritBeast);
    Task<InsertOrUpdateResult<bool>> UpdateUserSpiritBeastStarAsync(string userId, SpiritBeasts spiritBeast);
    Task<SpiritBeasts> GetUserSpiritBeastByIdAsync(string userId, string Id);
    Task<SpiritBeasts> SumPowerUserSpiritBeastsAsync(string userId);
}