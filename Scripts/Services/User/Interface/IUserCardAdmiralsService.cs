using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardAdmiralsService
{
    Task<List<CardAdmirals>> GetFinalPowerAsync(string user_id, List<CardAdmirals> CardAdmiralsList);
    Task<List<CardAdmirals>> GetScienceFictionPowerAsync(string user_id, List<CardAdmirals> CardAdmiralsList);
    Task<List<CardAdmirals>> GetAllEquipmentPowerAsync(string user_id, List<CardAdmirals> CardAdmiralsList);
    Task<List<CardAdmirals>> GetAllRankPowerAsync(string user_id, List<CardAdmirals> CardAdmiralsList);
    Task<List<CardAdmirals>> GetAllMasterPowerAsync(string user_id, List<CardAdmirals> CardAdmiralsList);
    Task<List<CardAdmirals>> GetAllAnimeStatsPowerAsync(string user_id, List<CardAdmirals> cardAdmirals);
    Task<List<CardAdmirals>> GetAllSpiritBeastPowerAsync(string user_id, List<CardAdmirals> cardAdmirals);
    Task<CardAdmirals> GetNewLevelPowerAsync(CardAdmirals c, double coefficient);
    Task<CardAdmirals> GetNewBreakthroughPowerAsync(CardAdmirals c, double coefficient);
    Task<List<CardAdmirals>> GetSkillsAsync(string user_id, List<CardAdmirals> CardAdmiralsList);
    Task<List<CardAdmirals>> GetUserCardAdmiralsAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<List<CardAdmirals>> GetUserCardAdmiralsTeamAsync(string user_id, string teamId, string position);
    Task<List<CardAdmirals>> GetUserCardAdmiralsTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardAdmiralsTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardAdmiralAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardAdmiralsCountAsync(string user_id, string type, string rare);
    Task<int> GetUserCardAdmiralsTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardAdmiralsTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardAdmiralAsync(CardAdmirals CardAdmirals);
    Task<bool> UpdateCardAdmiralLevelAsync(CardAdmirals cardAdmirals, int cardLevel);
    Task<bool> UpdateCardAdmiralBreakthroughAsync(CardAdmirals cardAdmirals, int star, double quantity);
    Task<CardAdmirals> GetUserCardAdmiralByIdAsync(string user_id, string Id);
    Task<List<CardAdmirals>> GetAllUserCardAdmiralsInTeamAsync(string user_id);
}