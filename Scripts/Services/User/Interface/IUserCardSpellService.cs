using System.Collections.Generic;

public interface IUserCardSpellService
{
    List<CardSpell> GetFinalPower(string user_id, List<CardSpell> CardSpellList);
    List<CardSpell> GetAllEquipmentPower(string user_id, List<CardSpell> CardSpellList);
    List<CardSpell> GetAllRankPower(string user_id, List<CardSpell> CardSpellList);
    List<CardSpell> GetAllAnimeStatsPower(string user_id, List<CardSpell> CardSpellList);
    CardSpell GetNewLevelPower(CardSpell c, double coefficient);
    CardSpell GetNewBreakthroughPower(CardSpell c, double coefficient);
    List<CardSpell> GetUserCardSpell(string user_id, string type, int pageSize, int offset, string rare);
    List<CardSpell> GetUserCardSpellTeam(string user_id, string teamId, string position);
    Dictionary<string, int> GetUniqueCardSpellTypesTeam(string teamId);
    bool UpdateTeamCardSpell(string team_id, string position, string card_id);
    int GetUserCardSpellCount(string user_id, string type, string rare);
    int GetUserCardSpellTeamsPositionCount(string user_id, string team_id, string position);
    int GetUserCardSpellTeamsCount(string user_id, string team_id);
    bool InsertUserCardSpell(CardSpell CardSpell);
    bool UpdateCardSpellLevel(CardSpell cardSpell, int cardLevel);
    bool UpdateCardSpellBreakthrough(CardSpell cardSpell, int star, int quantity);
    CardSpell GetUserCardSpellById(string user_id, string Id);
    List<CardSpell> GetAllUserCardSpellInTeam(string user_id);
}