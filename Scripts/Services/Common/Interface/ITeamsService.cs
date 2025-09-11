using System.Collections.Generic;
using MySql.Data.MySqlClient;

public interface ITeamsService
{
    List<Teams> GetUserTeams(string user_id);
    bool InsertUserTeams(string user_id, int team_number);
    int GetMaxTeamId(MySqlConnection connection);
    double GetTeamsPower(string user_id);
}
