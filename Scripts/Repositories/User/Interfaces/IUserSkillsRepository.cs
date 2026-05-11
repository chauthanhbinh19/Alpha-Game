using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSkillsRepository
{
    Task<List<Skills>> GetUserSkillsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserSkillsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserSkillAsync(Skills skill);
    Task<bool> InsertOrUpdateUserSkillsBatchAsync(List<Skills> skills);
    Task<bool> UpdateSkillLevelAsync(Skills skill, int level);
    Task<bool> UpdateSkillBreakthroughAsync(Skills skill, int star, double quantity);
    Task<Skills> GetUserSkillsByIdAsync(string user_id, string Id);
    Task<List<Skills>> GetUserCardHeroesSkillsAsync(string user_id, string cardId);
    Task<List<Skills>> GetUserCardCaptainsSkillsAsync(string user_id, string cardId);
    Task<List<Skills>> GetUserCardColonelsSkillsAsync(string user_id, string cardId);
    Task<List<Skills>> GetUserCardGeneralsSkillsAsync(string user_id, string cardId);
    Task<List<Skills>> GetUserCardAdmiralsSkillsAsync(string user_id, string cardId);
    Task<List<Skills>> GetUserCardMilitariesSkillsAsync(string user_id, string cardId);
    Task<List<Skills>> GetUserCardMonstersSkillsAsync(string user_id, string cardId);
    Task<List<Skills>> GetUserCardSpellsSkillsAsync(string user_id, string cardId);
    Task<List<Skills>> GetUserCardSoldiersSkillsAsync(string user_id, string cardId);
    Task<bool> InsertUserCardHeroSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> InsertUserCardCaptainSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> InsertUserCardColonelSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> InsertUserCardGeneralSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> InsertUserCardAdmiralSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> InsertUserCardMilitarySkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> InsertUserCardMonsterSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> InsertUserCardSpellSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> InsertUserCardSoldierSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> DeleteUserCardHeroSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> DeleteUserCardCaptainSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> DeleteUserCardColonelSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> DeleteUserCardGeneralSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> DeleteUserCardAdmiralSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> DeleteUserCardMonsterSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> DeleteUserCardMilitarySkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> DeleteUserCardSpellSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<bool> DeleteUserCardSoldierSkillsAsync(string userId, string cardId, string skillId, int position);
}