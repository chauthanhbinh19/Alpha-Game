using System.Collections.Generic;

public interface IUserSkillsRepository
{
    List<Skills> GetUserSkills(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserSkillsCount(string user_id, string type, string rare);
    bool InsertUserSkills(Skills skills);
    bool UpdateSkillsLevel(Skills skills, int cardLevel);
    bool UpdateSkillsBreakthrough(Skills skills, int star, double quantity);
    Skills GetUserSkillsById(string user_id, string id);
    List<Skills> GetUserCardHeroesSkills(string user_id, string cardId);
    List<Skills> GetUserCardCaptainsSkills(string user_id, string cardId);
    List<Skills> GetUserCardColonelsSkills(string user_id, string cardId);
    List<Skills> GetUserCardGeneralsSkills(string user_id, string cardId);
    List<Skills> GetUserCardAdmiralsSkills(string user_id, string cardId);
    List<Skills> GetUserCardMilitarySkills(string user_id, string cardId);
    List<Skills> GetUserCardMonstersSkills(string user_id, string cardId);
    List<Skills> GetUserCardSpellSkills(string user_id, string cardId);
    bool InsertUserCardHeroesSkills(string userId, string cardId, string skillId, int position);
    bool InsertUserCardCaptainsSkills(string userId, string cardId, string skillId, int position);
    bool InsertUserCardColonelsSkills(string userId, string cardId, string skillId, int position);
    bool InsertUserCardGeneralsSkills(string userId, string cardId, string skillId, int position);
    bool InsertUserCardAdmiralsSkills(string userId, string cardId, string skillId, int position);
    bool InsertUserCardMilitarySkills(string userId, string cardId, string skillId, int position);
    bool InsertUserCardMonstersSkills(string userId, string cardId, string skillId, int position);
    bool InsertUserCardSpellSkills(string userId, string cardId, string skillId, int position);
    bool DeleteUserCardHeroesSkills(string userId, string cardId, string skillId, int position);
    bool DeleteUserCardCaptainsSkills(string userId, string cardId, string skillId, int position);
    bool DeleteUserCardColonelsSkills(string userId, string cardId, string skillId, int position);
    bool DeleteUserCardGeneralsSkills(string userId, string cardId, string skillId, int position);
    bool DeleteUserCardAdmiralsSkills(string userId, string cardId, string skillId, int position);
    bool DeleteUserCardMonstersSkills(string userId, string cardId, string skillId, int position);
    bool DeleteUserCardMilitarySkills(string userId, string cardId, string skillId, int position);
    bool DeleteUserCardSpellSkills(string userId, string cardId, string skillId, int position);
}