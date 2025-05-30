using System.Collections.Generic;

public interface IUserCardColonelsRepository
{
    List<CardColonels> GetUserCardColonels(string user_id, string type, int pageSize, int offset);
    List<CardColonels> GetUserCardColonelsTeam(string user_id, string teamId, string position);
    Dictionary<string, int> GetUniqueCardColonelTypesTeam(string teamId);
    int GetUserCardColonelsCount(string user_id, string type);
    int GetUserCardColonelsTeamsPositionCount(string user_id, string team_id, string position);
    bool InsertUserCardColonels(CardColonels CardColonels);
    bool UpdateCardColonelsLevel(CardColonels cardColonels, int cardLevel);
    bool UpdateCardColonelsBreakthrough(CardColonels cardColonels, int star, int quantity);
    bool InsertFactCardColonels(CardColonels cardColonels);
    bool UpdateFactCardColonels(CardColonels cardColonels);
    bool UpdateTeamFactCardColonels(string team_id, string position, string card_id);
    CardColonels GetUserCardColonelsById(string user_id, string Id);
    List<CardColonels> GetAllUserCardColonelsInTeam(string user_id);
}