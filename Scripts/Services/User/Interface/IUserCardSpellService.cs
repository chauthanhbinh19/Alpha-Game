using System.Collections.Generic;

public interface IUserCardSpellService
{
    List<CardSpells> GetFinalPower(string user_id, List<CardSpells> CardSpellList);
    List<CardSpells> GetAllEquipmentPower(string user_id, List<CardSpells> CardSpellList);
    List<CardSpells> GetAllRankPower(string user_id, List<CardSpells> CardSpellList);
    List<CardSpells> GetAllAnimeStatsPower(string user_id, List<CardSpells> CardSpellList);
    CardSpells GetNewLevelPower(CardSpells c, double coefficient);
    CardSpells GetNewBreakthroughPower(CardSpells c, double coefficient);
    List<CardSpells> GetUserCardSpell(string user_id, string type, int pageSize, int offset, string rare);
    List<CardSpells> GetUserCardSpellTeam(string user_id, string teamId, string position);
    List<CardSpells> GetUserCardSpellTeamWithoutPosition(string user_id, string teamId);
    Dictionary<string, int> GetUniqueCardSpellTypesTeam(string teamId);
    bool UpdateTeamCardSpell(string team_id, string position, string card_id);
    int GetUserCardSpellCount(string user_id, string type, string rare);
    int GetUserCardSpellTeamsPositionCount(string user_id, string team_id, string position);
    int GetUserCardSpellTeamsCount(string user_id, string team_id);
    bool InsertUserCardSpell(CardSpells CardSpell);
    bool UpdateCardSpellLevel(CardSpells cardSpell, int cardLevel);
    bool UpdateCardSpellBreakthrough(CardSpells cardSpell, int star, int quantity);
    CardSpells GetUserCardSpellById(string user_id, string Id);
    List<CardSpells> GetAllUserCardSpellInTeam(string user_id);
}