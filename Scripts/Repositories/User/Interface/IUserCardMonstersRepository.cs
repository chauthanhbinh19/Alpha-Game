using System.Collections.Generic;

public interface IUserCardMonstersRepository
{
    List<CardMonsters> GetUserCardMonsters(string user_id, string type, int pageSize, int offset, string rare);
    List<CardMonsters> GetUserCardMonstersTeam(string user_id, string teamId, string position);
    Dictionary<string, int> GetUniqueCardMonsterTypesTeam(string teamId);
    int GetUserCardMonstersCount(string user_id, string type, string rare);
    int GetUserCardMonstersTeamsPositionCount(string user_id, string team_id, string position);
    int GetUserCardMonstersTeamsCount(string user_id, string team_id);
    bool InsertUserCardMonsters(CardMonsters CardMonsters);
    bool UpdateCardMonstersLevel(CardMonsters cardMonsters, int cardLevel);
    bool UpdateCardMonstersBreakthrough(CardMonsters cardMonsters, int star, int quantity);
    bool InsertFactCardMonsters(CardMonsters cardMonsters);
    bool UpdateFactCardMonsters(CardMonsters cardMonsters);
    bool UpdateTeamFactCardMonsters(string team_id, string position, string card_id);
    CardMonsters GetUserCardMonstersById(string user_id, string Id);
    List<CardMonsters> GetAllUserCardMonstersInTeam(string user_id);
}