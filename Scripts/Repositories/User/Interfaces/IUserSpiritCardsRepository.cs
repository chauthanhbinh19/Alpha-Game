using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSpiritCardsRepository
{
    Task<List<SpiritCards>> GetUserSpiritCardsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserSpiritCardsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserSpiritCardAsync(SpiritCards spiritCard);
    Task<bool> InsertOrUpdateUserSpiritCardsBatchAsync(List<SpiritCards> spiritCards);
    Task<bool> UpdateSpiritCardLevelAsync(SpiritCards spiritCard, int level);
    Task<bool> UpdateSpiritCardBreakthroughAsync(SpiritCards spiritCard, int star, double quantity);
    Task<SpiritCards> GetUserSpiritCardByIdAsync(string user_id, string Id);
    Task<SpiritCards> SumPowerUserSpiritCardsAsync();
}