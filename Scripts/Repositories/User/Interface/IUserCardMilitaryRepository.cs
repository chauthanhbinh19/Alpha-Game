using System.Collections.Generic;

public interface IUserCardMilitaryRepository
{
    List<CardMilitaries> GetUserCardMilitary(string user_id, string type, int pageSize, int offset, string rare);
    List<CardMilitaries> GetUserCardMilitaryTeam(string user_id, string teamId, string position);
    List<CardMilitaries> GetUserCardMilitaryTeamWithoutPosition(string user_id, string teamId);
    Dictionary<string, int> GetUniqueCardMilitaryTypesTeam(string teamId);
    bool UpdateTeamCardMilitary(string team_id, string position, string card_id);
    int GetUserCardMilitaryCount(string user_id, string type, string rare);
    int GetUserCardMilitaryTeamsPositionCount(string user_id, string team_id, string position);
    int GetUserCardMilitaryTeamsCount(string user_id, string team_id);
    bool InsertUserCardMilitary(CardMilitaries CardMilitary);
    bool UpdateCardMilitaryLevel(CardMilitaries cardMilitary, int cardLevel);
    bool UpdateCardMilitaryBreakthrough(CardMilitaries cardMilitary, int star, double quantity);
    CardMilitaries GetUserCardMilitaryById(string user_id, string Id);
    List<CardMilitaries> GetAllUserCardMilitaryInTeam(string user_id);
}