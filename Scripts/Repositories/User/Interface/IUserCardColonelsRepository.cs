using System.Collections.Generic;

public interface IUserCardColonelsRepository
{
    List<CardColonels> GetUserCardColonels(string user_id, string type, int pageSize, int offset, string rare);
    List<CardColonels> GetUserCardColonelsTeam(string user_id, string teamId, string position);
    Dictionary<string, int> GetUniqueCardColonelTypesTeam(string teamId);
    int GetUserCardColonelsCount(string user_id, string type, string rare);
    int GetUserCardColonelsTeamsPositionCount(string user_id, string team_id, string position);
    int GetUserCardColonelsTeamsCount(string user_id, string team_id);
    bool InsertUserCardColonels(CardColonels CardColonels);
    bool UpdateCardColonelsLevel(CardColonels cardColonels, int cardLevel);
    bool UpdateCardColonelsBreakthrough(CardColonels cardColonels, int star, int quantity);
    bool UpdateTeamCardColonels(string team_id, string position, string card_id);
    CardColonels GetUserCardColonelsById(string user_id, string Id);
    List<CardColonels> GetAllUserCardColonelsInTeam(string user_id);
}