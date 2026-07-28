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

    public async Task<List<Skills>> GetUserSkillsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Skills> list = await _userSkillsRepository.GetUserSkillsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<int> GetUserSkillsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userSkillsRepository.GetUserSkillsCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserSkillAsync(string userId, Skills skill)
    {
        var result = await _userSkillsRepository.InsertUserSkillAsync(userId, skill);
        if (result)
        {
            await SkillsGalleryService.Create().InsertSkillGalleryAsync(userId, skill.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserSkillLevelAsync(string userId, Skills skill)
    {
        return await _userSkillsRepository.UpdateUserSkillLevelAsync(userId, skill);
    }

    public async Task<bool> UpdateUserSkillStarAsync(string userId, Skills skill)
    {
        var result = await _userSkillsRepository.UpdateUserSkillStarAsync(userId, skill);
        if (result)
        {
            await SkillsGalleryService.Create().UpdateStarSkillGalleryAsync(userId, skill.Id, skill.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserSkillBreakthroughAsync(string userId, Skills skill, int star, double quantity)
    {
        return await _userSkillsRepository.UpdateUserSkillBreakthroughAsync(userId, skill, star, quantity);
    }

    public async Task<Skills> GetUserSkillsByIdAsync(string userId, string Id)
    {
        return await _userSkillsRepository.GetUserSkillsByIdAsync(userId, Id);
    }

    public async Task<List<Skills>> GetUserCardHeroesSkillsAsync(string userId, string cardId)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardHeroesSkillsAsync(userId, cardId);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardCaptainsSkillsAsync(string userId, string cardId)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardCaptainsSkillsAsync(userId, cardId);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardColonelsSkillsAsync(string userId, string cardId)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardColonelsSkillsAsync(userId, cardId);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardGeneralsSkillsAsync(string userId, string cardId)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardGeneralsSkillsAsync(userId, cardId);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardAdmiralsSkillsAsync(string userId, string cardId)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardAdmiralsSkillsAsync(userId, cardId);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardMilitariesSkillsAsync(string userId, string cardId)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardMilitariesSkillsAsync(userId, cardId);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardMonstersSkillsAsync(string userId, string cardId)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardMonstersSkillsAsync(userId, cardId);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardSpellsSkillsAsync(string userId, string cardId)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardSpellsSkillsAsync(userId, cardId);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardSoldiersSkillsAsync(string userId, string cardId)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardSoldiersSkillsAsync(userId, cardId);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardHeroesSkillsAsync(string userId, List<string> cardHeroIds)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardHeroesSkillsAsync(userId, cardHeroIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardCaptainsSkillsAsync(string userId, List<string> cardCaptainIds)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardCaptainsSkillsAsync(userId, cardCaptainIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardColonelsSkillsAsync(string userId, List<string> cardColonelIds)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardColonelsSkillsAsync(userId, cardColonelIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardGeneralsSkillsAsync(string userId, List<string> cardGeneralIds)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardGeneralsSkillsAsync(userId, cardGeneralIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardAdmiralsSkillsAsync(string userId, List<string> cardAdmiralIds)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardAdmiralsSkillsAsync(userId, cardAdmiralIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardMilitariesSkillsAsync(string userId, List<string> cardMilitaryIds)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardMilitariesSkillsAsync(userId, cardMilitaryIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardMonstersSkillsAsync(string userId, List<string> cardMonsterIds)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardMonstersSkillsAsync(userId, cardMonsterIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardSpellsSkillsAsync(string userId, List<string> cardSpellIds)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardSpellsSkillsAsync(userId, cardSpellIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
    }

    public async Task<List<Skills>> GetUserCardSoldiersSkillsAsync(string userId, List<string> cardSoldierIds)
    {
        List<Skills> list = await _userSkillsRepository.GetUserCardSoldiersSkillsAsync(userId, cardSoldierIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);

        foreach (var skill in list)
        {
            // Đọc trực tiếp từ hàm của Interface
            skill.Pattern = PatternsService.Create().GetPatternFromCache(skill.Pattern.Id);
        }

        return list;
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

    public async Task<bool> InsertOrUpdateUserSkillsBatchAsync(string userId, List<Skills> skills)
    {
        return await _userSkillsRepository.InsertOrUpdateUserSkillsBatchAsync(userId, skills);
    }

    public async Task<int> AssignRandomSkillsToUserCardHeroesAsync(string userId)
    {
        return await _userSkillsRepository.AssignRandomUserSkillsInternalAsync(userId, "user_card_heroes", "card_heroes_skills", "card_hero_id");
    }

    public async Task<int> AssignRandomSkillsToUserCardCaptainsAsync(string userId)
    {
        return await _userSkillsRepository.AssignRandomUserSkillsInternalAsync(userId, "user_card_captains", "card_captains_skills", "card_captain_id");
    }

    public async Task<int> AssignRandomSkillsToUserCardColonelsAsync(string userId)
    {
        return await _userSkillsRepository.AssignRandomUserSkillsInternalAsync(userId, "user_card_colonels", "card_colonels_skills", "card_colonel_id");
    }

    public async Task<int> AssignRandomSkillsToUserCardGeneralsAsync(string userId)
    {
        return await _userSkillsRepository.AssignRandomUserSkillsInternalAsync(userId, "user_card_generals", "card_generals_skills", "card_general_id");
    }

    public async Task<int> AssignRandomSkillsToUserCardAdmiralsAsync(string userId)
    {
        return await _userSkillsRepository.AssignRandomUserSkillsInternalAsync(userId, "user_card_admirals", "card_admirals_skills", "card_admiral_id");
    }

    public async Task<int> AssignRandomSkillsToUserCardMonstersAsync(string userId)
    {
        return await _userSkillsRepository.AssignRandomUserSkillsInternalAsync(userId, "user_card_monsters", "card_monsters_skills", "card_monster_id");
    }

    public async Task<int> AssignRandomSkillsToUserCardMilitariesAsync(string userId)
    {
        return await _userSkillsRepository.AssignRandomUserSkillsInternalAsync(userId, "user_card_militaries", "card_militaries_skills", "card_military_id");
    }

    public async Task<int> AssignRandomSkillsToUserCardSoldiersAsync(string userId)
    {
        return await _userSkillsRepository.AssignRandomUserSkillsInternalAsync(userId, "user_card_soldiers", "card_soldiers_skills", "card_soldier_id");
    }

    public async Task<int> AssignRandomSkillsToUserCardSpellsAsync(string userId)
    {
        return await _userSkillsRepository.AssignRandomUserSkillsInternalAsync(userId, "user_card_spells", "card_spells_skills", "card_spell_id");
    }

    public async Task<List<Skills>> GetUserCardsSkillsAsync(string userId, List<string> allCardIds)
    {
        return await _userSkillsRepository.GetUserCardsSkillsAsync(userId, allCardIds);
    }

    public async Task<List<Skills>> GetUserSkillsWithCardsAsync(string userId, List<string> heroIds, List<string> captainIds, List<string> colonelIds, List<string> generalIds, List<string> admiralIds, List<string> monsterIds, List<string> militaryIds, List<string> spellIds, List<string> soldierIds)
    {
        return await _userSkillsRepository.GetUserSkillsWithCardsAsync(userId, heroIds, captainIds, colonelIds, generalIds, admiralIds, monsterIds, militaryIds, spellIds, soldierIds);
    }
}
