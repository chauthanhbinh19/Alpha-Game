using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSkillsService : IUserSkillsService
{
     private static UserSkillsService _instance;
    private readonly IUserSkillsRepository _userSkillsRepository;

    public UserSkillsService(IUserSkillsRepository userSkillsRepository)
    {
        _userSkillsRepository = userSkillsRepository;
    }

    public static UserSkillsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserSkillsService(new UserSkillsRepository());
        }
        return _instance;
    }

    public async Task<List<Skills>> GetUserSkillsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Skills> list = await _userSkillsRepository.GetUserSkillsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSkillsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userSkillsRepository.GetUserSkillsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserSkillsAsync(Skills skill)
    {
        return await _userSkillsRepository.InsertUserSkillAsync(skill);
    }

    public async Task<bool> UpdateSkillsLevelAsync(Skills skill, int level)
    {
        return await _userSkillsRepository.UpdateSkillLevelAsync(skill, level);
    }

    public async Task<bool> UpdateSkillsBreakthroughAsync(Skills skill, int star, double quantity)
    {
        return await _userSkillsRepository.UpdateSkillBreakthroughAsync(skill, star, quantity);
    }

    public async Task<Skills> GetUserSkillsByIdAsync(string user_id, string Id)
    {
        return await _userSkillsRepository.GetUserSkillsByIdAsync(user_id, Id);
    }

    public async Task<List<Skills>> GetUserCardHeroesSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardHeroesSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardCaptainsSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardCaptainsSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardColonelsSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardColonelsSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardGeneralsSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardGeneralsSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardAdmiralsSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardAdmiralsSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardMilitariesSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardMilitariesSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardMonstersSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardMonstersSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardSpellsSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardSpellsSkillsAsync(user_id, cardId);
    }

    public async Task<List<Skills>> GetUserCardSoldiersSkillsAsync(string user_id, string cardId)
    {
        return await _userSkillsRepository.GetUserCardSoldiersSkillsAsync(user_id, cardId);
    }

    public async Task<bool> InsertUserCardHeroSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardHeroSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardCaptainSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardCaptainSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardColonelSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardColonelSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardGeneralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardGeneralSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardAdmiralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardAdmiralSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardMilitarySkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardMilitarySkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardMonsterSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardMonsterSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardSpellSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardSpellSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertUserCardSoldierSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.InsertUserCardSoldierSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardHeroSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardHeroSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardCaptainSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardCaptainSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardColonelSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardColonelSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardGeneralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardGeneralSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardAdmiralSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardAdmiralSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardMonsterSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardMonsterSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardMilitarySkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardMilitarySkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardSpellSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardSpellSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> DeleteUserCardSoldierSkillsAsync(string userId, string cardId, string skillId, int position)
    {
        return await _userSkillsRepository.DeleteUserCardSoldierSkillsAsync(userId, cardId, skillId, position);
    }

    public async Task<bool> InsertOrUpdateUserSkillsBatchAsync(List<Skills> skills)
    {
        return await _userSkillsRepository.InsertOrUpdateUserSkillsBatchAsync(skills);
    }
}
