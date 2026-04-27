using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardLivesService
{
    Task<List<CardLives>> GetUserCardLivesAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserCardLivesCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserCardLifeAsync(CardLives CardLife, string userId);
    Task<bool> UpdateCardLifeLevelAsync(CardLives CardLife, int cardLevel);
    Task<bool> UpdateCardLifeBreakthroughAsync(CardLives CardLife, int star, double quantity);
    Task<CardLives> GetUserCardLifeByIdAsync(string user_id, string Id);
    Task<CardLives> SumPowerUserCardLivesAsync();
}