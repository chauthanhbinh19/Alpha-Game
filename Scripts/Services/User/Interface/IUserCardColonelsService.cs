using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUseCardColonelsService
{
    Task<List<CardColonels>> GetFinalPowerAsync(string user_id, List<CardColonels> CardColonelsList);
    Task<List<CardColonels>> GetScienceFictionPowerAsync(string user_id, List<CardColonels> CardColonelsList);
    Task<List<CardColonels>> GetAllEquipmentPowerAsync(string user_id, List<CardColonels> CardColonelsList);
    Task<List<CardColonels>> GetAllRankPowerAsync(string user_id, List<CardColonels> CardColonelsList);
    Task<List<CardColonels>> GetAllMasterPowerAsync(string user_id, List<CardColonels> CardColonelsList);
    Task<List<CardColonels>> GetAllAnimeStatsPowerAsync(string user_id, List<CardColonels> CardColonelsList);
    Task<List<CardColonels>> GetAllSpiritBeastPowerAsync(string user_id, List<CardColonels> cardColonels);
    Task<CardColonels> GetNewLevelPowerAsync(CardColonels c, double coefficient);
    Task<CardColonels> GetNewBreakthroughPowerAsync(CardColonels c, double coefficient);
    Task<List<CardColonels>> GetSkillsAsync(string user_id, List<CardColonels> CardColonelsList);
    Task<List<CardColonels>> GetUserCardColonelsAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<List<CardColonels>> GetUserCardColonelsTeamAsync(string user_id, string teamId, string position);
    Task<List<CardColonels>> GetUserCardColonelsTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardColonelsTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardColonelAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardColonelsCountAsync(string user_id, string type, string rare);
    Task<int> GetUserCardColonelsTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardColonelsTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardColonelAsync(CardColonels CardColonels);
    Task<bool> UpdateCardColonelLevelAsync(CardColonels CardColonels, int cardLevel);
    Task<bool> UpdateCardColonelBreakthroughAsync(CardColonels CardColonels, int star, double quantity);
    Task<CardColonels> GetUserCardColonelByIdAsync(string user_id, string Id);
    Task<List<CardColonels>> GetAllUserCardColonelsInTeamAsync(string user_id);
}