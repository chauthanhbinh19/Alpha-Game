using System.Collections.Generic;

public interface IUserCardMonstersService
{
    List<CardMonsters> GetFinalPower(string user_id, List<CardMonsters> CardMonstersList);
    List<CardMonsters> GetAllEquipmentPower(string user_id, List<CardMonsters> CardMonstersList);
    List<CardMonsters> GetAllRankPower(string user_id, List<CardMonsters> CardMonstersList);
    List<CardMonsters> GetAllAnimeStatsPower(string user_id, List<CardMonsters> CardMonstersList);
    CardMonsters GetNewLevelPower(CardMonsters c, double coefficient);
    CardMonsters GetNewBreakthroughPower(CardMonsters c, double coefficient);
    List<CardMonsters> GetUserCardMonsters(string user_id, string type, int pageSize, int offset);
    List<CardMonsters> GetUserCardMonstersTeam(string user_id, string teamId, string position);
    Dictionary<string, int> GetUniqueCardMonsterTypesTeam(string teamId);
    int GetUserCardMonstersCount(string user_id, string type);
    int GetUserCardMonstersTeamsPositionCount(string user_id, string team_id, string position);
    bool InsertUserCardMonsters(CardMonsters CardMonsters);
    bool UpdateCardMonstersLevel(CardMonsters cardMonsters, int cardLevel);
    bool UpdateCardMonstersBreakthrough(CardMonsters cardMonsters, int star, int quantity);
    bool InsertFactCardMonsters(CardMonsters cardMonsters);
    bool UpdateFactCardMonsters(CardMonsters cardMonsters);
    bool UpdateTeamFactCardMonsters(string team_id, string position, string card_id);
    CardMonsters GetUserCardMonstersById(string user_id, string Id);
    List<CardMonsters> GetAllUserCardMonstersInTeam(string user_id);
}