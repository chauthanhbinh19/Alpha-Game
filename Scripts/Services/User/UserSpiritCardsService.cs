using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSpiritCardsService : IUserSpiritCardsService
{
     private static UserSpiritCardsService _instance;
    private readonly IUserSpiritCardsRepository _userSpiritCardsRepository;

    public UserSpiritCardsService(IUserSpiritCardsRepository userSpiritCardsRepository)
    {
        _userSpiritCardsRepository = userSpiritCardsRepository;
    }

    public static UserSpiritCardsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserSpiritCardsService(new UserSpiritCardsRepository());
        }
        return _instance;
    }

    public async Task<List<SpiritCards>> GetUserSpiritCardAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> list = await _userSpiritCardsRepository.GetUserSpiritCardsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<SpiritCards>> GetAllUserSpiritCardAsync(string user_id, int pageSize, int offset)
    {
        List<SpiritCards> list = await _userSpiritCardsRepository.GetAllUserSpiritCardsAsync(user_id, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<List<SpiritCards>> GetSpiritCardsByCardIdsAsync(string user_id, List<string> cardIds)
    {
        List<SpiritCards> list = await _userSpiritCardsRepository.GetSpiritCardsByCardIdsAsync(user_id, cardIds);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSpiritCardCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userSpiritCardsRepository.GetUserSpiritCardsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserSpiritCardAsync(SpiritCards SpiritCard)
    {
        return await _userSpiritCardsRepository.InsertUserSpiritCardAsync(SpiritCard);
    }

    public async Task<bool> UpdateSpiritCardLevelAsync(SpiritCards SpiritCard, int cardLevel)
    {
        return await _userSpiritCardsRepository.UpdateSpiritCardLevelAsync(SpiritCard, cardLevel);
    }

    public async Task<bool> UpdateSpiritCardBreakthroughAsync(SpiritCards SpiritCard, int star, double quantity)
    {
        return await _userSpiritCardsRepository.UpdateSpiritCardBreakthroughAsync(SpiritCard, star, quantity);
    }

    public async Task<SpiritCards> GetUserSpiritCardByIdAsync(string user_id, string Id)
    {
        return await _userSpiritCardsRepository.GetUserSpiritCardByIdAsync(user_id, Id);
    }

    public async Task<SpiritCards> SumPowerUserSpiritCardsAsync()
    {
        return await _userSpiritCardsRepository.SumPowerUserSpiritCardsAsync();
    }

    public async Task<bool> InsertOrUpdateUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHeroes, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.InsertOrUpdateUserCardHeroSpiritCardAsync(userId, cardHeroes, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptains, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.InsertOrUpdateUserCardCaptainSpiritCardAsync(userId, cardCaptains, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonels, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.InsertOrUpdateUserCardColonelSpiritCardAsync(userId, cardColonels, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGenerals, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.InsertOrUpdateUserCardGeneralSpiritCardAsync(userId, cardGenerals, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmirals, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.InsertOrUpdateUserCardAdmiralSpiritCardAsync(userId, cardAdmirals, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitary, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.InsertOrUpdateUserCardMilitarySpiritCardAsync(userId, cardMilitary, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonsters, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.InsertOrUpdateUserCardMonsterSpiritCardAsync(userId, cardMonsters, spiritBeast);
    }

    public async Task<bool> InsertOrUpdateUserCardSpellSpiritCardAsync(string userId, CardSpells cardSpell, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.InsertOrUpdateUserCardSpellSpiritCardAsync(userId, cardSpell, spiritBeast);
    }

    public async Task<List<SpiritCards>> GetAllUserCardHeroesSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardsRepository.GetAllUserCardHeroesSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardCaptainsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardsRepository.GetAllUserCardCaptainsSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardColonelsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardsRepository.GetAllUserCardColonelsSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardGeneralsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardsRepository.GetAllUserCardGeneralsSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardAdmiralsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardsRepository.GetAllUserCardAdmiralsSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardMilitariesSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardsRepository.GetAllUserCardMilitariesSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardMonstersSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardsRepository.GetAllUserCardMonstersSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<List<SpiritCards>> GetAllUserCardSpellsSpiritCardAsync(string user_id, int pageSize, int offset, string status)
    {
        return await _userSpiritCardsRepository.GetAllUserCardSpellsSpiritCardAsync(user_id, pageSize, offset, status);
    }

    public async Task<SpiritCards> GetUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHeroes)
    {
        return await _userSpiritCardsRepository.GetUserCardHeroSpiritCardAsync(userId, cardHeroes);
    }

    public async Task<SpiritCards> GetUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptains)
    {
        return await _userSpiritCardsRepository.GetUserCardCaptainSpiritCardAsync(userId, cardCaptains);
    }

    public async Task<SpiritCards> GetUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonels)
    {
        return await _userSpiritCardsRepository.GetUserCardColonelSpiritCardAsync(userId, cardColonels);
    }

    public async Task<SpiritCards> GetUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGenerals)
    {
        return await _userSpiritCardsRepository.GetUserCardGeneralSpiritCardAsync(userId, cardGenerals);
    }

    public async Task<SpiritCards> GetUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmirals)
    {
        return await _userSpiritCardsRepository.GetUserCardAdmiralSpiritCardAsync(userId, cardAdmirals);
    }

    public async Task<SpiritCards> GetUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitary)
    {
        return await _userSpiritCardsRepository.GetUserCardMilitarySpiritCardAsync(userId, cardMilitary);
    }

    public async Task<SpiritCards> GetUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonsters)
    {
        return await _userSpiritCardsRepository.GetUserCardMonsterSpiritCardAsync(userId, cardMonsters);
    }

    public async Task<SpiritCards> GetUserCardSpellSpiritCardAsync(string userId, CardSpells cardSpell)
    {
        return await _userSpiritCardsRepository.GetUserCardSpellSpiritCardAsync(userId, cardSpell);
    }

    public async Task<bool> DeleteUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHeroes, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.DeleteUserCardHeroSpiritCardAsync(userId, cardHeroes, spiritBeast);
    }

    public async Task<bool> DeleteUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptains, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.DeleteUserCardCaptainSpiritCardAsync(userId, cardCaptains, spiritBeast);
    }

    public async Task<bool> DeleteUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonels, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.DeleteUserCardColonelSpiritCardAsync(userId, cardColonels, spiritBeast);
    }

    public async Task<bool> DeleteUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGenerals, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.DeleteUserCardGeneralSpiritCardAsync(userId, cardGenerals, spiritBeast);
    }

    public async Task<bool> DeleteUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmirals, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.DeleteUserCardAdmiralSpiritCardAsync(userId, cardAdmirals, spiritBeast);
    }

    public async Task<bool> DeleteUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitary, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.DeleteUserCardMilitarySpiritCardAsync(userId, cardMilitary, spiritBeast);
    }

    public async Task<bool> DeleteUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonsters, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.DeleteUserCardMonsterSpiritCardAsync(userId, cardMonsters, spiritBeast);
    }
    
    public async Task<bool> DeleteUserCardSpellSpiritCardAsync(string userId, CardSpells cardSpell, SpiritCards spiritBeast)
    {
        return await _userSpiritCardsRepository.DeleteUserCardSpellSpiritCardAsync(userId, cardSpell, spiritBeast);
    }
}
