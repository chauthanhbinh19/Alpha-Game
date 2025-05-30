using System.Collections.Generic;

public interface IUserCardAdmiralsService
{
    List<CardAdmirals> GetFinalPower(string user_id, List<CardAdmirals> CardAdmiralsList);
    List<CardAdmirals> GetAllEquipmentPower(string user_id, List<CardAdmirals> CardAdmiralsList);
    List<CardAdmirals> GetAllRankPower(string user_id, List<CardAdmirals> CardAdmiralsList);
    List<CardAdmirals> GetAllAnimeStatsPower(string user_id, List<CardAdmirals> cardAdmirals);
    CardAdmirals GetNewLevelPower(CardAdmirals c, double coefficient);
    CardAdmirals GetNewBreakthroughPower(CardAdmirals c, double coefficient);
    List<CardAdmirals> GetUserCardAdmirals(string user_id, string type, int pageSize, int offset);
    List<CardAdmirals> GetUserCardAdmiralsTeam(string user_id, string teamId, string position);
    Dictionary<string, int> GetUniqueCardAdmiralTypesTeam(string teamId);
    bool UpdateTeamFactCardAdmirals(string team_id, string position, string card_id);
    int GetUserCardAdmiralsCount(string user_id, string type);
    int GetUserCardAdmiralsTeamsPositionCount(string user_id, string team_id, string position);
    bool InsertUserCardAdmirals(CardAdmirals CardAdmirals);
    bool UpdateCardAdmiralsLevel(CardAdmirals cardAdmirals, int cardLevel);
    bool UpdateCardAdmiralsBreakthrough(CardAdmirals cardAdmirals, int star, int quantity);
    bool InsertFactCardAdmirals(CardAdmirals cardAdmirals);
    bool UpdateFactCardAdmirals(CardAdmirals cardAdmirals);
    CardAdmirals GetUserCardAdmiralsById(string user_id, string Id);
    List<CardAdmirals> GetAllUserCardAdmiralsInTeam(string user_id);
}