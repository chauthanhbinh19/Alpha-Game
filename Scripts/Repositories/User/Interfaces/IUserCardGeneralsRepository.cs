using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardGeneralsRepository
{
    Task<List<CardGenerals>> GetUserCardGeneralsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardGenerals>> GetUserCardGeneralsTeamAsync(string userId, string teamId, string position);
    Task<List<CardGenerals>> GetUserCardGeneralsTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardGeneralsTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardGeneralAsync(string userId, string team_id, string position, string cardId);
    Task<bool> IsCardInTeamAsync(string userId, string cardId);
    Task<int> GetUserCardGeneralsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardGeneralsTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardGeneralsTeamsCountAsync(string userId, string team_id);
    Task<InsertOrUpdateResult<CardGenerals>> InsertOrUpdateUserCardGeneralAsync(string userId, CardGenerals cardGeneral);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<CardGenerals>>> InsertOrUpdateUserCardGeneralsBatchAsync(string userId, List<CardGenerals> cardGenerals);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardGeneralLevelAsync(string userId, CardGenerals cardGeneral);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardGeneralStarAsync(string userId, CardGenerals cardGeneral);
    Task<CardGenerals> GetUserCardGeneralByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId);
    Task<BaseStats> GetTeamTotalStatsWithoutQualityAsync(string userId);
}