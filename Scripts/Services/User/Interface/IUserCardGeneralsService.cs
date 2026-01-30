using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardGeneralsService
{
    Task<List<CardGenerals>> GetFinalPowerAsync(string user_id, List<CardGenerals> CardGeneralsList);
    Task<List<CardGenerals>> GetScienceFictionPowerAsync(string user_id, List<CardGenerals> CardGeneralsList);
    Task<List<CardGenerals>> GetAllEquipmentPowerAsync(string user_id, List<CardGenerals> CardGeneralsList);
    Task<List<CardGenerals>> GetAllRankPowerAsync(string user_id, List<CardGenerals> CardGeneralsList);
    Task<List<CardGenerals>> GetAllMasterPowerAsync(string user_id, List<CardGenerals> CardGeneralsList);
    Task<List<CardGenerals>> GetAllAnimeStatsPowerAsync(string user_id, List<CardGenerals> CardGeneralsList);
    Task<List<CardGenerals>> GetAllSpiritBeastPowerAsync(string user_id, List<CardGenerals> cardGenerals);
    Task<CardGenerals> GetNewLevelPowerAsync(CardGenerals c, double coefficient);
    Task<CardGenerals> GetNewBreakthroughPowerAsync(CardGenerals c, double coefficient);
    Task<List<CardGenerals>> GetSkillsAsync(string user_id, List<CardGenerals> CardGeneralsList);
    Task<List<CardGenerals>> GetUserCardGeneralsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardGenerals>> GetUserCardGeneralsTeamAsync(string user_id, string teamId, string position);
    Task<List<CardGenerals>> GetUserCardGeneralsTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardGeneralsTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardGeneralAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardGeneralsCountAsync(string user_id, string search, string type, string rare);
    Task<int> GetUserCardGeneralsTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardGeneralsTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardGeneralAsync(CardGenerals CardGenerals);
    Task<bool> UpdateCardGeneralLevelAsync(CardGenerals CardGenerals, int cardLevel);
    Task<bool> UpdateCardGeneralBreakthroughAsync(CardGenerals CardGenerals, int star, double quantity);
    Task<CardGenerals> GetUserCardGeneralByIdAsync(string user_id, string Id);
    Task<List<CardGenerals>> GetAllUserCardGeneralsInTeamAsync(string user_id);
}