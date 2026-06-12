using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

public interface ITeamsRepository
{
    Task<List<Teams>> GetUserTeamsAsync(string user_id);
    Task<bool> InsertUserTeamsAsync(string user_id, int team_number = 1);
    Task<int> GetMaxTeamIdAsync(MySqlConnection connection);
    double GetTeamsPower(string user_id);
    Task<List<TeamEmblems>> GetUserTeamEmblemsAsync(string user_id, string team_id, int position, string cardType);
    Task<bool> InsertUserTeamEmblemsAsync(string user_id, string teamId, int position, EmblemDTO emblemDTO);
    Task<bool> DeleteUserTeamEmblemsAsync(string user_id, string teamId, int position, string cardType);
    Task<bool> UpdateUserCardHeroesTeamPositionsAsync(string userId);
    Task<bool> UpdateUserCardCaptainsTeamPositionsAsync(string userId);
    Task<bool> UpdateUserCardColonelsTeamPositionsAsync(string userId);
    Task<bool> UpdateUserCardGeneralsTeamPositionsAsync(string userId);
    Task<bool> UpdateUserCardAdmiralsTeamPositionsAsync(string userId);
    Task<bool> UpdateUserCardMonstersTeamPositionsAsync(string userId);
    Task<bool> UpdateUserCardMilitariesTeamPositionsAsync(string userId);
    Task<bool> UpdateUserCardSoldiersTeamPositionsAsync(string userId);
    Task<bool> UpdateUserCardSpellsTeamPositionsAsync(string userId);
}
