using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSpiritCardsRepository
{
    Task<List<SpiritCards>> GetUserSpiritCardsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserSpiritCardsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<SpiritCards>> InsertOrUpdateUserSpiritCardAsync(string userId, SpiritCards spiritCard);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<SpiritCards>>> InsertOrUpdateUserSpiritCardsBatchAsync(string userId, List<SpiritCards> spiritCards);
    Task<InsertOrUpdateResult<bool>> UpdateUserSpiritCardLevelAsync(string userId, SpiritCards spiritCard);
    Task<InsertOrUpdateResult<bool>> UpdateUserSpiritCardStarAsync(string userId, SpiritCards spiritCard);
    Task<SpiritCards> GetUserSpiritCardByIdAsync(string userId, string Id);
    Task<SpiritCards> SumPowerUserSpiritCardsAsync(string userId);
}