using System.Collections.Generic;

public interface IUseCardColonelsService
{
    List<CardColonels> GetFinalPower(string user_id, List<CardColonels> CardColonelsList);
    List<CardColonels> GetAllEquipmentPower(string user_id, List<CardColonels> CardColonelsList);
    List<CardColonels> GetAllRankPower(string user_id, List<CardColonels> CardColonelsList);
    List<CardColonels> GetAllAnimeStatsPower(string user_id, List<CardColonels> CardColonelsList);
    CardColonels GetNewLevelPower(CardColonels c, double coefficient);
    CardColonels GetNewBreakthroughPower(CardColonels c, double coefficient);
    List<CardColonels> GetSkills(string user_id, List<CardColonels> CardColonelsList);
    List<CardColonels> GetUserCardColonels(string user_id, string type, int pageSize, int offset, string rare);
    List<CardColonels> GetUserCardColonelsTeam(string user_id, string teamId, string position);
    List<CardColonels> GetUserCardColonelsTeamWithoutPosition(string user_id, string teamId);
    Dictionary<string, int> GetUniqueCardColonelTypesTeam(string teamId);
    int GetUserCardColonelsCount(string user_id, string type, string rare);
    int GetUserCardColonelsTeamsPositionCount(string user_id, string team_id, string position);
    int GetUserCardColonelsTeamsCount(string user_id, string team_id);
    bool InsertUserCardColonels(CardColonels CardColonels);
    bool UpdateCardColonelsLevel(CardColonels cardColonels, int cardLevel);
    bool UpdateCardColonelsBreakthrough(CardColonels cardColonels, int star, double quantity);
    bool UpdateTeamCardColonels(string team_id, string position, string card_id);
    CardColonels GetUserCardColonelsById(string user_id, string Id);
    List<CardColonels> GetAllUserCardColonelsInTeam(string user_id);
}