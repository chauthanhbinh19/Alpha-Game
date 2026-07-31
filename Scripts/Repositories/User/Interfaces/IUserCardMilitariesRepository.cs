using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardMilitariesRepository
{
    Task<List<CardMilitaries>> GetUserCardMilitariesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardMilitaries>> GetUserCardMilitariesTeamAsync(string userId, string teamId, string position);
    Task<List<CardMilitaries>> GetUserCardMilitariesTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardMilitariesTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardMilitaryAsync(string userId, string team_id, string position, string cardId);
    Task<bool> IsCardInTeamAsync(string userId, string cardId);
    Task<int> GetUserCardMilitariesCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardMilitariesTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardMilitariesTeamsCountAsync(string userId, string team_id);
    Task<InsertOrUpdateResult<CardMilitaries>> InsertOrUpdateUserCardMilitaryAsync(string userId, CardMilitaries cardMilitary);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<CardMilitaries>>> InsertOrUpdateUserCardMilitariesBatchAsync(string userId, List<CardMilitaries> cardMilitaries);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardMilitaryLevelAsync(string userId, CardMilitaries cardMilitary);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardMilitaryStarAsync(string userId, CardMilitaries cardMilitary);
    Task<CardMilitaries> GetUserCardMilitaryByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId);
    Task<BaseStats> GetTeamTotalStatsWithoutQualityAsync(string userId);
}