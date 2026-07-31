using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardCaptainsRepository
{
    Task<List<CardCaptains>> GetUserCardCaptainsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardCaptains>> GetUserCardCaptainsTeamAsync(string userId, string teamId, string position);
    Task<List<CardCaptains>> GetUserCardCaptainsTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardCaptainsTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardCaptainAsync(string userId, string team_id, string position, string cardId);
    Task<bool> IsCardInTeamAsync(string userId, string cardId);
    Task<int> GetUserCardCaptainsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardCaptainsTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardCaptainsTeamsCountAsync(string userId, string team_id);
    Task<InsertOrUpdateResult<CardCaptains>> InsertOrUpdateUserCardCaptainAsync(string userId, CardCaptains cardCaptain);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<CardCaptains>>> InsertOrUpdateUserCardCaptainsBatchAsync(string userId, List<CardCaptains> cardCaptains);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardCaptainLevelAsync(string userId, CardCaptains cardCaptain);
    Task<InsertOrUpdateResult<bool>> UpdateUserCardCaptainStarAsync(string userId, CardCaptains cardCaptain);
    Task<CardCaptains> GetUserCardCaptainByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId);
    Task<BaseStats> GetTeamTotalStatsWithoutQualityAsync(string userId);
}