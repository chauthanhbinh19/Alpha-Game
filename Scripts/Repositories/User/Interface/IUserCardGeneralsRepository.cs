using System.Collections.Generic;

public interface IUserCardGeneralsRepository
{
    List<CardGenerals> GetUserCardGenerals(string user_id, string type, int pageSize, int offset, string rare);
    List<CardGenerals> GetUserCardGeneralsTeam(string user_id, string teamId, string position);
    List<CardGenerals> GetUserCardGeneralsTeamWithoutPosition(string user_id, string teamId);
    int GetUserCardGeneralsTeamsPositionCount(string user_id, string team_id, string position);
    Dictionary<string, int> GetUniqueCardGeneralTypesTeam(string teamId);
    bool UpdateTeamCardGenerals(string team_id, string position, string card_id);
    int GetUserCardGeneralsCount(string user_id, string type, string rare);
    int GetUserCardGeneralsTeamsCount(string user_id, string team_id);
    bool InsertUserCardGenerals(CardGenerals CardGenerals);
    bool UpdateCardGeneralsLevel(CardGenerals cardGenerals, int cardLevel);
    bool UpdateCardGeneralsBreakthrough(CardGenerals cardGenerals, int star, double quantity);
    CardGenerals GetUserCardGeneralsById(string user_id, string Id);
    List<CardGenerals> GetAllUserCardGeneralsInTeam(string user_id);
}