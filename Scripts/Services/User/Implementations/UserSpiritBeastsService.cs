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

    public async Task<List<SpiritBeasts>> GetUserSpiritBeastsAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetUserSpiritBeastsAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<SpiritBeasts>> GetAllUserSpiritBeastsAsync(string userId, int pageSize, int offset)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetAllUserSpiritBeastsAsync(userId, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<SpiritBeasts>> GetSpiritBeastsByCardIdsAsync(string userId, List<string> cardIds)
    {
        List<SpiritBeasts> list = await _userSpiritBeastsRepository.GetUserSpiritBeastsByCardIdsAsync(userId, cardIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSpiritBeastsCountAsync(string userId, string search, string rare)
    {
        return await _userSpiritBeastsRepository.GetUserSpiritBeastsCountAsync(userId, search, rare);
    }

    public async Task<bool> InsertUserSpiritBeastAsync(string userId, SpiritBeasts spiritBeast)
    {
        var result = await _userSpiritBeastsRepository.InsertUserSpiritBeastAsync(userId, spiritBeast);
        if (result)
        {
            await SpiritBeastsGalleryService.Create().InsertSpiritBeastGalleryAsync(userId, spiritBeast.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserSpiritBeastLevelAsync(string userId, SpiritBeasts spiritBeast)
    {
        return await _userSpiritBeastsRepository.UpdateUserSpiritBeastLevelAsync(userId, spiritBeast);
    }

    public async Task<bool> UpdateUserSpiritBeastStarAsync(string userId, SpiritBeasts spiritBeast)
    {
        var result = await _userSpiritBeastsRepository.UpdateUserSpiritBeastStarAsync(userId, spiritBeast);
        if (result)
        {
            await SpiritBeastsGalleryService.Create().UpdateStarSpiritBeastGalleryAsync(userId, spiritBeast.Id, spiritBeast.Star);
        }
        return result;
    }
    
    public async Task<bool> UpdateUserSpiritBeastBreakthroughAsync(string userId, SpiritBeasts spiritBeast, int star, double quantity)
    {
        return await _userSpiritBeastsRepository.UpdateUserSpiritBeastBreakthroughAsync(userId, spiritBeast, star, quantity);
    }

    public async Task<SpiritBeasts> GetUserSpiritBeastByIdAsync(string userId, string Id)
    {
        return await _userSpiritBeastsRepository.GetUserSpiritBeastByIdAsync(userId, Id);
    }

    public async Task<SpiritBeasts> SumPowerUserSpiritBeastsAsync(string userId)
    {
        return await _userSpiritBeastsRepository.SumPowerUserSpiritBeastsAsync(userId);
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

    public async Task<List<SpiritBeasts>> GetAllUserCardHeroesSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardHeroesSpiritBeastAsync(userId, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardCaptainsSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardCaptainsSpiritBeastAsync(userId, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardColonelsSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardColonelsSpiritBeastAsync(userId, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardGeneralsSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardGeneralsSpiritBeastAsync(userId, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardAdmiralsSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardAdmiralsSpiritBeastAsync(userId, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardMilitariesSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardMilitariesSpiritBeastAsync(userId, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardMonstersSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardMonstersSpiritBeastAsync(userId, pageSize, offset, status);
    }

    public async Task<List<SpiritBeasts>> GetAllUserCardSpellsSpiritBeastAsync(string userId, int pageSize, int offset, string status)
    {
        return await _userSpiritBeastsRepository.GetAllUserCardSpellsSpiritBeastAsync(userId, pageSize, offset, status);
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

    public async Task<bool> InsertOrUpdateUserSpiritBeastsBatchAsync(string userId, List<SpiritBeasts> spiritBeasts)
    {
        return await _userSpiritBeastsRepository.InsertOrUpdateUserSpiritBeastsBatchAsync(userId, spiritBeasts);
    }
}
