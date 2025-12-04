using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardMilitariesRepository
{
    Task<List<CardMilitaries>> GetUserCardMilitariesAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<List<CardMilitaries>> GetUserCardMilitariesTeamAsync(string user_id, string teamId, string position);
    Task<List<CardMilitaries>> GetUserCardMilitariesTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardMilitariesTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardMilitaryAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardMilitariesCountAsync(string user_id, string type, string rare);
    Task<int> GetUserCardMilitariesTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardMilitariesTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardMilitaryAsync(CardMilitaries CardMilitaries);
    Task<bool> UpdateCardMilitaryLevelAsync(CardMilitaries CardMilitaries, int cardLevel);
    Task<bool> UpdateCardMilitaryBreakthroughAsync(CardMilitaries CardMilitaries, int star, double quantity);
    Task<CardMilitaries> GetUserCardMilitaryByIdAsync(string user_id, string Id);
    Task<List<CardMilitaries>> GetAllUserCardMilitariesInTeamAsync(string user_id);
}