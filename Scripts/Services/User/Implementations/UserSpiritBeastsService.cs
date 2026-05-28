using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSpiritBeastsService : IUserSpiritBeastsService
{
     private static UserSpiritBeastsService _instance;
    private readonly IUserSpiritBeastsRepository _userSpiritBeastsRepository;

    public UserSpiritBeastsService(IUserSpiritBeastsRepository userSpiritBeastsRepository)
    {
        _userSpiritBeastsRepository = userSpiritBeastsRepository;
    }

    public static UserSpiritBeastsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserSpiritBeastsService(new UserSpiritBeastsRepository());
        }
        return _instance;
    }

    public async Task<List<SpiritBeasts>> GetUserSpiritBeastsAsync(string user_id, string search, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetUserSpiritBeastsAsync(user_id, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<SpiritBeasts>> GetAllUserSpiritBeastsAsync(string user_id, int pageSize, int offset)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetAllUserSpiritBeastsAsync(user_id, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsByCardIdsAsync(string user_id, List<string> cardIds)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetSpiritBeastsByCardIdsAsync(user_id, cardIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSpiritBeastsCountAsync(string user_id, string search, string rare)
    {
        return await _userSpiritBeastsRepository.GetUserSpiritBeastsCountAsync(user_id, search, rare);
    }

    public async Task<bool> InsertUserSpiritBeastAsync(SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertUserSpiritBeastAsync(spiritBeast);
    }

    public async Task<bool> UpdateSpiritBeastLevelAsync(SpiritBeasts spiritBeast, int level)
    {
        return await _userSpiritBeastsRepository.UpdateSpiritBeastLevelAsync(spiritBeast, level);
    }

    public async Task<bool> UpdateSpiritBeastBreakthroughAsync(SpiritBeasts spiritBeast, int star, double quantity)
    {
        return await _userSpiritBeastsRepository.UpdateSpiritBeastBreakthroughAsync(spiritBeast, star, quantity);
    }

    public async Task<SpiritBeasts> GetUserSpiritBeastByIdAsync(string user_id, string Id)
    {
        return await _userSpiritBeastsRepository.GetUserSpiritBeastByIdAsync(user_id, Id);
    }

    public async Task<SpiritBeasts> SumPowerUserSpiritBeastsAsync()
    {
        return await _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync();
    }

    public async Task<bool> InsertOrUpdateUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHero, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardHeroSpiritBeastAsync(userId, cardHero, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptain, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardCaptainSpiritBeastAsync(userId, cardCaptain, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonel, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardColonelSpiritBeastAsync(userId, cardColonel, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGeneral, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardGeneralSpiritBeastAsync(userId, cardGeneral, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmiral, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardAdmiralSpiritBeastAsync(userId, cardAdmiral, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitary, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardMilitarySpiritBeastAsync(userId, cardMilitary, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonster, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardMonsterSpiritBeastAsync(userId, cardMonster, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpell, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserCardSpellSpiritBeastAsync(userId, cardSpell, spiritBeast);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardHeroesSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardHeroesSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardCaptainsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardCaptainsSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardColonelsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardColonelsSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardGeneralsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardGeneralsSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardAdmiralsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardAdmiralsSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardMilitariesSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardMilitariesSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardMonstersSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardMonstersSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardSpellsSpiritBeastAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardSpellsSpiritBeastAsync(user_id, pageSize, offset, status);
    }

    public async Task<SpiritBeasts> GetUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHero)
    {
        return await _userSpiritBeastsRepository.GetUserCardHeroSpiritBeastAsync(userId, cardHero);
    }

    public async Task<SpiritBeasts> GetUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptain)
    {
        return await _userSpiritBeastsRepository.GetUserCardCaptainSpiritBeastAsync(userId, cardCaptain);
    }

    public async Task<SpiritBeasts> GetUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonel)
    {
        return await _userSpiritBeastsRepository.GetUserCardColonelSpiritBeastAsync(userId, cardColonel);
    }

    public async Task<SpiritBeasts> GetUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGeneral)
    {
        return await _userSpiritBeastsRepository.GetUserCardGeneralSpiritBeastAsync(userId, cardGeneral);
    }

    public async Task<SpiritBeasts> GetUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmiral)
    {
        return await _userSpiritBeastsRepository.GetUserCardAdmiralSpiritBeastAsync(userId, cardAdmiral);
    }

    public async Task<SpiritBeasts> GetUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitary)
    {
        return await _userSpiritBeastsRepository.GetUserCardMilitarySpiritBeastAsync(userId, cardMilitary);
    }

    public async Task<SpiritBeasts> GetUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonster)
    {
        return await _userSpiritBeastsRepository.GetUserCardMonsterSpiritBeastAsync(userId, cardMonster);
    }

    public async Task<SpiritBeasts> GetUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpell)
    {
        return await _userSpiritBeastsRepository.GetUserCardSpellSpiritBeastAsync(userId, cardSpell);
    }

    public async Task<bool> DeleteUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHero, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardHeroSpiritBeastAsync(userId, cardHero, spiritBeast);
    }

    public async Task<bool> DeleteUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptain, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardCaptainSpiritBeastAsync(userId, cardCaptain, spiritBeast);
    }

    public async Task<bool> DeleteUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonel, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardColonelSpiritBeastAsync(userId, cardColonel, spiritBeast);
    }

    public async Task<bool> DeleteUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGeneral, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardGeneralSpiritBeastAsync(userId, cardGeneral, spiritBeast);
    }

    public async Task<bool> DeleteUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmiral, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardAdmiralSpiritBeastAsync(userId, cardAdmiral, spiritBeast);
    }

    public async Task<bool> DeleteUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitary, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardMilitarySpiritBeastAsync(userId, cardMilitary, spiritBeast);
    }

    public async Task<bool> DeleteUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonster, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardMonsterSpiritBeastAsync(userId, cardMonster, spiritBeast);
    }
    
    public async Task<bool> DeleteUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpell, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.DeleteUserCardSpellSpiritBeastAsync(userId, cardSpell, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserSpiritBeastsBatchAsync(List<SpiritBeasts> spiritBeasts)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserSpiritBeastsBatchAsync(spiritBeasts);
    }
}
