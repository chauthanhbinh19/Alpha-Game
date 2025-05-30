using System.Collections.Generic;

public interface IUserCardMilitaryRepository
{
    List<CardMilitary> GetUserCardMilitary(string user_id, string type, int pageSize, int offset);
    List<CardMilitary> GetUserCardMilitaryTeam(string user_id, string teamId, string position);
    Dictionary<string, int> GetUniqueCardMilitaryTypesTeam(string teamId);
    bool UpdateTeamFactCardMilitary(string team_id, string position, string card_id);
    int GetUserCardMilitaryCount(string user_id, string type);
    int GetUserCardMilitaryTeamsPositionCount(string user_id, string team_id, string position);
    bool InsertUserCardMilitary(CardMilitary CardMilitary);
    bool UpdateCardMilitaryLevel(CardMilitary cardMilitary, int cardLevel);
    bool UpdateCardMilitaryBreakthrough(CardMilitary cardMilitary, int star, int quantity);
    bool InsertFactCardMilitary(CardMilitary cardMilitary);
    bool UpdateFactCardMilitary(CardMilitary cardMilitary);
    CardMilitary GetUserCardMilitaryById(string user_id, string Id);
    List<CardMilitary> GetAllUserCardMilitaryInTeam(string user_id);
}