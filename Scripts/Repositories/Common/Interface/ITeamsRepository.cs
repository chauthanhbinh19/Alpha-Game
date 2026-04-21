using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

public interface ITeamsRepository
{
    Task<List<Teams>> GetUserTeamsAsync(string user_id);
    Task<bool> InsertUserTeamsAsync(string user_id, int team_number = 1);
    Task<int> GetMaxTeamIdAsync(MySqlConnection connection);
    double GetTeamsPower(string user_id);
    Task<bool> InsertUserTeamEmblemsAsync(string user_id, EmblemDTO emblemDTO);
    Task<bool> DeleteUserTeamEmblemsAsync(string user_id, EmblemDTO emblemDTO);
}
