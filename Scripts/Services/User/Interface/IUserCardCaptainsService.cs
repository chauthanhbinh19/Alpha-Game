using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardCaptainsService
{
    Task<List<CardCaptains>> GetFinalPowerAsync(string user_id, List<CardCaptains> CardCaptainsList);
    Task<List<CardCaptains>> GetScienceFictionPowerAsync(string user_id, List<CardCaptains> CardCaptainsList);
    Task<List<CardCaptains>> GetAllEquipmentPowerAsync(string user_id, List<CardCaptains> CardCaptainsList);
    Task<List<CardCaptains>> GetAllRankPowerAsync(string user_id, List<CardCaptains> CardCaptainsList);
    Task<List<CardCaptains>> GetAllMasterPowerAsync(string user_id, List<CardCaptains> CardCaptainsList);
    Task<List<CardCaptains>> GetAllAnimeStatsPowerAsync(string user_id, List<CardCaptains> CardCaptainsList);
    Task<List<CardCaptains>> GetAllSpiritBeastPowerAsync(string user_id, List<CardCaptains> cardCaptains);
    Task<CardCaptains> GetNewLevelPowerAsync(CardCaptains c, double coefficient);
    Task<CardCaptains> GetNewBreakthroughPowerAsync(CardCaptains c, double coefficient);
    Task<List<CardCaptains>> GetSkillsAsync(string user_id, List<CardCaptains> CardCaptainsList);
    Task<List<CardCaptains>> GetUserCardCaptainsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardCaptains>> GetUserCardCaptainsTeamAsync(string user_id, string teamId, string position);
    Task<List<CardCaptains>> GetUserCardCaptainsTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardCaptainsTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardCaptainAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardCaptainsCountAsync(string user_id, string search, string type, string rare);
    Task<int> GetUserCardCaptainsTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardCaptainsTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardCaptainAsync(CardCaptains CardCaptains);
    Task<bool> UpdateCardCaptainLevelAsync(CardCaptains CardCaptains, int cardLevel);
    Task<bool> UpdateCardCaptainBreakthroughAsync(CardCaptains CardCaptains, int star, double quantity);
    Task<CardCaptains> GetUserCardCaptainByIdAsync(string user_id, string Id);
    Task<List<CardCaptains>> GetAllUserCardCaptainsInTeamAsync(string user_id);
}