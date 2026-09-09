using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSkillsService
{
    Task<List<Skills>> GetUserSkillsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserSkillsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSkillAsync(string userId, Skills skill);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserSkillsBatchAsync(string userId, List<Skills> skills);
    Task<InsertOrUpdateResult<bool>> UpdateUserSkillLevelAsync(string userId, Skills skill);
    Task<InsertOrUpdateResult<bool>> UpdateUserSkillStarAsync(string userId, Skills skill);
    Task<Skills> GetUserSkillsByIdAsync(string userId, string Id);
    Task<List<Skills>> GetUserCardHeroesSkillsAsync(string userId, string cardId);
    Task<List<Skills>> GetUserCardCaptainsSkillsAsync(string userId, string cardId);
    Task<List<Skills>> GetUserCardColonelsSkillsAsync(string userId, string cardId);
    Task<List<Skills>> GetUserCardGeneralsSkillsAsync(string userId, string cardId);
    Task<List<Skills>> GetUserCardAdmiralsSkillsAsync(string userId, string cardId);
    Task<List<Skills>> GetUserCardMilitariesSkillsAsync(string userId, string cardId);
    Task<List<Skills>> GetUserCardMonstersSkillsAsync(string userId, string cardId);
    Task<List<Skills>> GetUserCardSpellsSkillsAsync(string userId, string cardId);
    Task<List<Skills>> GetUserCardSoldiersSkillsAsync(string userId, string cardId);
    Task<List<Skills>> GetUserCardHeroesSkillsAsync(string userId, List<string> cardHeroIds);
    Task<List<Skills>> GetUserCardCaptainsSkillsAsync(string userId, List<string> cardCaptainIds);
    Task<List<Skills>> GetUserCardColonelsSkillsAsync(string userId, List<string> cardColonelIds);
    Task<List<Skills>> GetUserCardGeneralsSkillsAsync(string userId, List<string> cardGeneralIds);
    Task<List<Skills>> GetUserCardAdmiralsSkillsAsync(string userId, List<string> cardAdmiralIds);
    Task<List<Skills>> GetUserCardMilitariesSkillsAsync(string userId, List<string> cardMilitaryIds);
    Task<List<Skills>> GetUserCardMonstersSkillsAsync(string userId, List<string> cardMonsterIds);
    Task<List<Skills>> GetUserCardSpellsSkillsAsync(string userId, List<string> cardSpellIds);
    Task<List<Skills>> GetUserCardSoldiersSkillsAsync(string userId, List<string> cardSoldierIds);
    Task<List<Skills>> GetUserSkillsWithCardsAsync(
        string userId,
        List<string> heroIds,
        List<string> captainIds,
        List<string> colonelIds,
        List<string> generalIds,
        List<string> admiralIds,
        List<string> monsterIds,
        List<string> militaryIds,
        List<string> spellIds,
        List<string> soldierIds);
    Task<List<Skills>> GetUserCardsSkillsAsync(string userId, List<string> allCardIds);
    Task<InsertOrUpdateResult<bool>> InsertUserCardHeroSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> InsertUserCardCaptainSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> InsertUserCardColonelSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> InsertUserCardGeneralSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> InsertUserCardAdmiralSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> InsertUserCardMilitarySkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> InsertUserCardMonsterSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> InsertUserCardSpellSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> InsertUserCardSoldierSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> DeleteUserCardHeroSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> DeleteUserCardCaptainSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> DeleteUserCardColonelSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> DeleteUserCardGeneralSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> DeleteUserCardAdmiralSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> DeleteUserCardMonsterSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> DeleteUserCardMilitarySkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> DeleteUserCardSpellSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<InsertOrUpdateResult<bool>> DeleteUserCardSoldierSkillsAsync(string userId, string cardId, string skillId, int position);
    Task<int> AssignRandomSkillsToUserCardHeroesAsync(string userId);
    Task<int> AssignRandomSkillsToUserCardCaptainsAsync(string userId);
    Task<int> AssignRandomSkillsToUserCardColonelsAsync(string userId);
    Task<int> AssignRandomSkillsToUserCardGeneralsAsync(string userId);
    Task<int> AssignRandomSkillsToUserCardAdmiralsAsync(string userId);
    Task<int> AssignRandomSkillsToUserCardMonstersAsync(string userId);
    Task<int> AssignRandomSkillsToUserCardMilitariesAsync(string userId);
    Task<int> AssignRandomSkillsToUserCardSoldiersAsync(string userId);
    Task<int> AssignRandomSkillsToUserCardSpellsAsync(string userId);
}