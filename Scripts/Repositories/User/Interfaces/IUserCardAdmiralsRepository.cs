using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardAdmiralsRepository
{
    Task<List<CardAdmirals>> GetUserCardAdmiralsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardAdmirals>> GetUserCardAdmiralsTeamAsync(string userId, string teamId, string position);
    Task<List<CardAdmirals>> GetUserCardAdmiralsTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardAdmiralsTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardAdmiralAsync(string userId, string team_id, string position, string cardId);
    Task<int> GetUserCardAdmiralsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardAdmiralsTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardAdmiralsTeamsCountAsync(string userId, string team_id);
    Task<bool> InsertUserCardAdmiralAsync(string userId, CardAdmirals cardAdmiral);
    Task<bool> InsertOrUpdateUserCardAdmiralsBatchAsync(string userId, List<CardAdmirals> cardAdmirals);
    Task<bool> UpdateUserCardAdmiralLevelAsync(string userId, CardAdmirals cardAdmiral);
    Task<bool> UpdateUserCardAdmiralStarAsync(string userId, CardAdmirals cardAdmiral);
    Task<bool> UpdateUserCardAdmiralBreakthroughAsync(string userId, CardAdmirals cardAdmiral, int star, double quantity);
    Task<CardAdmirals> GetUserCardAdmiralByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId);
    Task<BaseStats> GetTeamTotalStatsWithoutQualityAsync(string userId);
}