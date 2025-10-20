using System.Collections.Generic;

public interface IUserCardMilitaryService
{
    List<CardMilitaries> GetFinalPower(string user_id, List<CardMilitaries> CardMilitaryList);
    List<CardMilitaries> GetAllEquipmentPower(string user_id, List<CardMilitaries> CardMilitaryList);
    List<CardMilitaries> GetAllRankPower(string user_id, List<CardMilitaries> CardMilitaryList);
    List<CardMilitaries> GetAllAnimeStatsPower(string user_id, List<CardMilitaries> CardMilitaryList);
    CardMilitaries GetNewLevelPower(CardMilitaries c, double coefficient);
    CardMilitaries GetNewBreakthroughPower(CardMilitaries c, double coefficient);
    List<CardMilitaries> GetUserCardMilitary(string user_id, string type, int pageSize, int offset, string rare);
    List<CardMilitaries> GetUserCardMilitaryTeam(string user_id, string teamId, string position);
    Dictionary<string, int> GetUniqueCardMilitaryTypesTeam(string teamId);
    bool UpdateTeamCardMilitary(string team_id, string position, string card_id);
    int GetUserCardMilitaryCount(string user_id, string type, string rare);
    int GetUserCardMilitaryTeamsPositionCount(string user_id, string team_id, string position);
    int GetUserCardMilitaryTeamsCount(string user_id, string team_id);
    bool InsertUserCardMilitary(CardMilitaries CardMilitary);
    bool UpdateCardMilitaryLevel(CardMilitaries cardMilitary, int cardLevel);
    bool UpdateCardMilitaryBreakthrough(CardMilitaries cardMilitary, int star, int quantity);
    CardMilitaries GetUserCardMilitaryById(string user_id, string Id);
    List<CardMilitaries> GetAllUserCardMilitaryInTeam(string user_id);
}