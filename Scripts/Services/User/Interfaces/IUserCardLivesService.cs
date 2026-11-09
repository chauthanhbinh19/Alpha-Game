using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardLivesService
{
    Task<List<CardLives>> GetUserCardLivesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserCardLivesCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardLifeAsync(string userId, CardLives cardLife);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCardLivesBatchAsync(string userId, List<CardLives> cardLives);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardLifeLevelAsync(string userId, CardLives cardLife, List<RecipeItemDetailDTO> totalDeductions);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardLifeStarAsync(string userId, CardLives cardLife);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardLifeQuantityAsync(string userId, CardLives cardLife, List<RecipeItemDetailDTO> totalDeductions);
    Task<CardLives> GetUserCardLifeByIdAsync(string userId, string Id);
    Task<CardLives> SumPowerUserCardLivesAsync(string userId);
}