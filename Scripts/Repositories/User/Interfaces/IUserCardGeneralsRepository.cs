using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardGeneralsRepository
{
    Task<List<CardGenerals>> GetUserCardGeneralsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardGenerals>> GetUserCardGeneralsTeamAsync(string userId, string teamId, string position);
    Task<List<CardGenerals>> GetUserCardGeneralsTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardGeneralsTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardGeneralAsync(string userId, string team_id, string position, string cardId);
    Task<int> GetUserCardGeneralsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardGeneralsTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardGeneralsTeamsCountAsync(string userId, string team_id);
    Task<bool> InsertUserCardGeneralAsync(string userId, CardGenerals cardGeneral);
    Task<bool> InsertOrUpdateUserCardGeneralsBatchAsync(string userId, List<CardGenerals> cardGenerals);
    Task<bool> UpdateUserCardGeneralLevelAsync(string userId, CardGenerals cardGeneral);
    Task<bool> UpdateUserCardGeneralStarAsync(string userId, CardGenerals cardGeneral);
    Task<bool> UpdateUserCardGeneralBreakthroughAsync(string userId, CardGenerals cardGeneral, int star, double quantity);
    Task<CardGenerals> GetUserCardGeneralByIdAsync(string userId, string Id);
    Task<List<CardGenerals>> GetAllUserCardGeneralsInTeamAsync(string userId);
}