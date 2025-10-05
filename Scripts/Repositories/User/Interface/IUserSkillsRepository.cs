using System.Collections.Generic;

public interface IUserSkillsRepository
{
    List<Skills> GetUserSkills(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserSkillsCount(string user_id, string type, string rare);
    bool InsertUserSkills(Skills skills);
    bool UpdateSkillsLevel(Skills skills, int cardLevel);
    bool UpdateSkillsBreakthrough(Skills skills, int star, int quantity);
    Skills GetUserSkillsById(string user_id, string id);
    List<Skills> GetUserCardHeroesSkills(string user_id, string cardId);
    List<Skills> GetUserCardCaptainsSkills(string user_id, string cardId);
    List<Skills> GetUserCardColonelsSkills(string user_id, string cardId);
    List<Skills> GetUserCardGeneralsSkills(string user_id, string cardId);
    List<Skills> GetUserCardAdmiralsSkills(string user_id, string cardId);
    List<Skills> GetUserCardMilitarySkills(string user_id, string cardId);
    List<Skills> GetUserCardMonstersSkills(string user_id, string cardId);
    List<Skills> GetUserCardSpellSkills(string user_id, string cardId);
}