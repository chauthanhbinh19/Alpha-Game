using System.Collections.Generic;

public interface IUserCardAdmiralsRepository
{
    List<CardAdmirals> GetUserCardAdmirals(string user_id, string type, int pageSize, int offset, string rare);
    List<CardAdmirals> GetUserCardAdmiralsTeam(string user_id, string teamId, string position);
    List<CardAdmirals> GetUserCardAdmiralsTeamWithoutPosition(string user_id, string teamId);
    Dictionary<string, int> GetUniqueCardAdmiralTypesTeam(string teamId);
    bool UpdateTeamCardAdmirals(string team_id, string position, string card_id);
    int GetUserCardAdmiralsCount(string user_id, string type, string rare);
    int GetUserCardAdmiralsTeamsPositionCount(string user_id, string team_id, string position);
    int GetUserCardAdmiralsTeamsCount(string user_id, string team_id);
    bool InsertUserCardAdmirals(CardAdmirals CardAdmirals);
    bool UpdateCardAdmiralsLevel(CardAdmirals cardAdmirals, int cardLevel);
    bool UpdateCardAdmiralsBreakthrough(CardAdmirals cardAdmirals, int star, int quantity);
    CardAdmirals GetUserCardAdmiralsById(string user_id, string Id);
    List<CardAdmirals> GetAllUserCardAdmiralsInTeam(string user_id);
}