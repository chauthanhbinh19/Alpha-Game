using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

public interface ITeamsService
{
    Task<List<Teams>> GetUserTeamsAsync(string user_id);
    Task<bool> InsertUserTeamsAsync(string user_id, int team_number);
    // int GetMaxTeamId(MySqlConnection connection);
    Task<double> GetTeamsPowerAsync(string user_id);
    Task<List<TeamEmblems>> GetUserTeamEmblemsAsync(string user_id, string team_id, int position, string cardType);
    Task<bool> InsertUserTeamEmblemsAsync(string user_id, string teamId, int position, EmblemDTO emblemDTO);
    Task<bool> DeleteUserTeamEmblemsAsync(string user_id, string teamId, int position, string cardType);
}
