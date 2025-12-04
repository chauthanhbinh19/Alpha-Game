using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardsService
{
    Task<Cards> GetNewLevelPowerAsync(Cards c, double coefficient);
    Task<Cards> GetNewBreakthroughPowerAsync(Cards c, double coefficient);
    Task<List<Cards>> GetUserCardsAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserCardsCountAsync(string user_id, string rare);
    Task<bool> InsertUserCardAsync(Cards Cards, string userId);
    Task<bool> UpdateCardLevelAsync(Cards Cards, int cardLevel);
    Task<bool> UpdateCardBreakthroughAsync(Cards Cards, int star, double quantity);
    Task<Cards> GetUserCardByIdAsync(string user_id, string Id);
    Task<Cards> SumPowerUserCardsAsync();
}