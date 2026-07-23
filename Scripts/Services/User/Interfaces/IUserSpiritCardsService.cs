using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSpiritCardsService
{
    Task<List<SpiritCards>> GetUserSpiritCardAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserSpiritCardCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserSpiritCardAsync(string userId, SpiritCards spiritCard);
    Task<bool> InsertOrUpdateUserSpiritCardsBatchAsync(string userId, List<SpiritCards> spiritCards);
    Task<bool> UpdateUserSpiritCardLevelAsync(string userId, SpiritCards spiritCard);
    Task<bool> UpdateUserSpiritCardStarAsync(string userId, SpiritCards spiritCard);
    Task<bool> UpdateUserSpiritCardBreakthroughAsync(string userId, SpiritCards spiritCard, int star, double quantity);
    Task<SpiritCards> GetUserSpiritCardByIdAsync(string userId, string Id);
    Task<SpiritCards> SumPowerUserSpiritCardsAsync(string userId);
}