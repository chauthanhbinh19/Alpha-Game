using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardLivesRepository
{
    Task<List<CardLives>> GetUserCardLivesAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserCardLivesCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserCardLifeAsync(CardLives cardLife, string userId);
    Task<bool> InsertOrUpdateUserCardLivesBatchAsync(List<CardLives> cardLives);
    Task<bool> UpdateCardLifeLevelAsync(CardLives cardLife, int level);
    Task<bool> UpdateCardLifeBreakthroughAsync(CardLives cardLife, int star, double quantity);
    Task<CardLives> GetUserCardLifeByIdAsync(string user_id, string Id);
    Task<CardLives> SumPowerUserCardLivesAsync();
}