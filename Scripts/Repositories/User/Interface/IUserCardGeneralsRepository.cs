using System.Collections.Generic;

public interface IUserCardGeneralsRepository
{
    List<CardGenerals> GetUserCardGenerals(string user_id, string type, int pageSize, int offset);
    List<CardGenerals> GetUserCardGeneralsTeam(string user_id, string teamId, string position);
    int GetUserCardGeneralsTeamsPositionCount(string user_id, string team_id, string position);
    Dictionary<string, int> GetUniqueCardGeneralTypesTeam(string teamId);
    bool UpdateTeamFactCardGenerals(string team_id, string position, string card_id);
    int GetUserCardGeneralsCount(string user_id, string type);
    bool InsertUserCardGenerals(CardGenerals CardGenerals);
    bool UpdateCardGeneralsLevel(CardGenerals cardGenerals, int cardLevel);
    bool UpdateCardGeneralsBreakthrough(CardGenerals cardGenerals, int star, int quantity);
    bool InsertFactCardGenerals(CardGenerals cardGenerals);
    bool UpdateFactCardGenerals(CardGenerals cardGenerals);
    CardGenerals GetUserCardGeneralsById(string user_id, string Id);
    List<CardGenerals> GetAllUserCardGeneralsInTeam(string user_id);
}