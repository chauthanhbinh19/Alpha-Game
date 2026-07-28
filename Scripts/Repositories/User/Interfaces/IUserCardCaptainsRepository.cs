using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardCaptainsRepository
{
    Task<List<CardCaptains>> GetUserCardCaptainsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardCaptains>> GetUserCardCaptainsTeamAsync(string userId, string teamId, string position);
    Task<List<CardCaptains>> GetUserCardCaptainsTeamWithoutPositionAsync(string userId, string teamId);
    Task<Dictionary<string, int>> GetUniqueUserCardCaptainsTypesTeamAsync(string userId, string teamId);
    Task<bool> UpdateTeamUserCardCaptainAsync(string userId, string team_id, string position, string cardId);
    Task<int> GetUserCardCaptainsCountAsync(string userId, string search, string type, string rare);
    Task<int> GetUserCardCaptainsTeamsPositionCountAsync(string userId, string team_id, string position);
    Task<int> GetUserCardCaptainsTeamsCountAsync(string userId, string team_id);
    Task<bool> InsertUserCardCaptainAsync(string userId, CardCaptains cardCaptain);
    Task<bool> InsertOrUpdateUserCardCaptainsBatchAsync(string userId, List<CardCaptains> cardCaptains);
    Task<bool> UpdateUserCardCaptainLevelAsync(string userId, CardCaptains cardCaptain);
    Task<bool> UpdateUserCardCaptainStarAsync(string userId, CardCaptains cardCaptain);
    Task<bool> UpdateUserCardCaptainBreakthroughAsync(string userId, CardCaptains cardCaptain, int star, double quantity);
    Task<CardCaptains> GetUserCardCaptainByIdAsync(string userId, string Id);
    Task<BaseStats> GetTeamTotalStatsAsync(string userId);
    Task<BaseStats> GetTeamTotalStatsWithoutQualityAsync(string userId);
}