using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSpiritBeastsService
{
    Task<List<SpiritBeasts>> GetUserSpiritBeastsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserSpiritBeastsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritBeastAsync(string userId, SpiritBeasts spiritBeast);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSpiritBeastsBatchAsync(string userId, List<SpiritBeasts> spiritBeasts);
    Task<bool> UpdateUserSpiritBeastLevelAsync(string userId, SpiritBeasts spiritBeast);
    Task<bool> UpdateUserSpiritBeastStarAsync(string userId, SpiritBeasts spiritBeast);
    Task<SpiritBeasts> GetUserSpiritBeastByIdAsync(string userId, string Id);
    Task<SpiritBeasts> SumPowerUserSpiritBeastsAsync(string userId);
}