using System.Collections.Generic;

public interface IUserCardMonstersService
{
    List<CardMonsters> GetFinalPower(string user_id, List<CardMonsters> CardMonstersList);
    List<CardMonsters> GetAllEquipmentPower(string user_id, List<CardMonsters> CardMonstersList);
    List<CardMonsters> GetAllRankPower(string user_id, List<CardMonsters> CardMonstersList);
    List<CardMonsters> GetAllAnimeStatsPower(string user_id, List<CardMonsters> CardMonstersList);
    CardMonsters GetNewLevelPower(CardMonsters c, double coefficient);
    CardMonsters GetNewBreakthroughPower(CardMonsters c, double coefficient);
    List<CardMonsters> GetUserCardMonsters(string user_id, string type, int pageSize, int offset, string rare);
    List<CardMonsters> GetUserCardMonstersTeam(string user_id, string teamId, string position);
    List<CardMonsters> GetUserCardMonstersTeamWithoutPosition(string user_id, string teamId);
    Dictionary<string, int> GetUniqueCardMonsterTypesTeam(string teamId);
    int GetUserCardMonstersCount(string user_id, string type, string rare);
    int GetUserCardMonstersTeamsPositionCount(string user_id, string team_id, string position);
    int GetUserCardMonstersTeamsCount(string user_id, string team_id);
    bool InsertUserCardMonsters(CardMonsters CardMonsters);
    bool UpdateCardMonstersLevel(CardMonsters cardMonsters, int cardLevel);
    bool UpdateCardMonstersBreakthrough(CardMonsters cardMonsters, int star, int quantity);
    bool UpdateTeamCardMonsters(string team_id, string position, string card_id);
    CardMonsters GetUserCardMonstersById(string user_id, string Id);
    List<CardMonsters> GetAllUserCardMonstersInTeam(string user_id);
}