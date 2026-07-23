using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardLivesService
{
    Task<List<CardLives>> GetUserCardLivesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserCardLivesCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserCardLifeAsync(CardLives cardLife, string userId);
    Task<bool> InsertOrUpdateUserCardLivesBatchAsync(string userId, List<CardLives> cardLives);
    Task<bool> UpdateUserCardLifeLevelAsync(string userId, CardLives cardLife);
    Task<bool> UpdateUserCardLifeStarAsync(string userId, CardLives cardLife);
    Task<bool> UpdateUserCardLifeBreakthroughAsync(string userId, CardLives cardLife, int star, double quantity);
    Task<CardLives> GetUserCardLifeByIdAsync(string userId, string Id);
    Task<CardLives> SumPowerUserCardLivesAsync(string userId);
}