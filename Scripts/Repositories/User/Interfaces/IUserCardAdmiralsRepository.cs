using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserCardAdmiralsRepository
{
    Task<List<CardAdmirals>> GetUserCardAdmiralsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<List<CardAdmirals>> GetUserCardAdmiralsTeamAsync(string user_id, string teamId, string position);
    Task<List<CardAdmirals>> GetUserCardAdmiralsTeamWithoutPositionAsync(string user_id, string teamId);
    Task<Dictionary<string, int>> GetUniqueCardAdmiralsTypesTeamAsync(string teamId);
    Task<bool> UpdateTeamCardAdmiralAsync(string team_id, string position, string card_id);
    Task<int> GetUserCardAdmiralsCountAsync(string user_id, string search, string type, string rare);
    Task<int> GetUserCardAdmiralsTeamsPositionCountAsync(string user_id, string team_id, string position);
    Task<int> GetUserCardAdmiralsTeamsCountAsync(string user_id, string team_id);
    Task<bool> InsertUserCardAdmiralAsync(CardAdmirals cardAdmiral);
    Task<bool> InsertOrUpdateUserCardAdmiralsBatchAsync(List<CardAdmirals> cardAdmirals);
    Task<bool> UpdateCardAdmiralLevelAsync(CardAdmirals cardAdmiral, int cardLevel);
    Task<bool> UpdateCardAdmiralBreakthroughAsync(CardAdmirals cardAdmiral, int star, double quantity);
    Task<CardAdmirals> GetUserCardAdmiralByIdAsync(string user_id, string Id);
    Task<List<CardAdmirals>> GetAllUserCardAdmiralsInTeamAsync(string user_id);
}