using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSpiritCardsRepository
{
    Task<List<SpiritCards>> GetUserSpiritCardsAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<List<SpiritCards>> GetAllUserSpiritCardsAsync(string user_id, int pageSize, int offset);
    Task<int> GetUserSpiritCardsCountAsync(string user_id, string type, string rare);
    Task<bool> InsertUserSpiritCardAsync(SpiritCards SpiritCard);
    Task<bool> UpdateSpiritCardLevelAsync(SpiritCards SpiritCard, int cardLevel);
    Task<bool> UpdateSpiritCardBreakthroughAsync(SpiritCards SpiritCard, int star, double quantity);
    Task<SpiritCards> GetUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHeroes);
    Task<SpiritCards> GetUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptains);
    Task<SpiritCards> GetUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonels);
    Task<SpiritCards> GetUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGenerals);
    Task<SpiritCards> GetUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmirals);
    Task<SpiritCards> GetUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitary);
    Task<SpiritCards> GetUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonsters);
    Task<SpiritCards> GetUserCardSpellSpiritCardAsync(string userId, CardSpells cardSpell);
    Task<bool> InsertOrUpdateUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHeroes, SpiritCards SpiritCard);
    Task<bool> InsertOrUpdateUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptains, SpiritCards SpiritCard);
    Task<bool> InsertOrUpdateUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonels, SpiritCards SpiritCard);
    Task<bool> InsertOrUpdateUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGenerals, SpiritCards SpiritCard);
    Task<bool> InsertOrUpdateUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmirals, SpiritCards SpiritCard);
    Task<bool> InsertOrUpdateUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitary, SpiritCards SpiritCard);
    Task<bool> InsertOrUpdateUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonsters, SpiritCards SpiritCard);
    Task<bool> InsertOrUpdateUserCardSpellSpiritCardAsync(string userId, CardSpells cardSpell, SpiritCards SpiritCard);
    Task<List<SpiritCards>> GetAllUserCardHeroesSpiritCardAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritCards>> GetAllUserCardCaptainsSpiritCardAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritCards>> GetAllUserCardColonelsSpiritCardAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritCards>> GetAllUserCardGeneralsSpiritCardAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritCards>> GetAllUserCardAdmiralsSpiritCardAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritCards>> GetAllUserCardMilitariesSpiritCardAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritCards>> GetAllUserCardMonstersSpiritCardAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritCards>> GetAllUserCardSpellsSpiritCardAsync(string user_id, int pageSize, int offset, string status);
    Task<bool> DeleteUserCardHeroSpiritCardAsync(string userId, CardHeroes cardHeroes, SpiritCards SpiritCard);
    Task<bool> DeleteUserCardCaptainSpiritCardAsync(string userId, CardCaptains cardCaptains, SpiritCards SpiritCard);
    Task<bool> DeleteUserCardColonelSpiritCardAsync(string userId, CardColonels cardColonels, SpiritCards SpiritCard);
    Task<bool> DeleteUserCardGeneralSpiritCardAsync(string userId, CardGenerals cardGenerals, SpiritCards SpiritCard);
    Task<bool> DeleteUserCardAdmiralSpiritCardAsync(string userId, CardAdmirals cardAdmirals, SpiritCards SpiritCard);
    Task<bool> DeleteUserCardMilitarySpiritCardAsync(string userId, CardMilitaries cardMilitaries, SpiritCards SpiritCard);
    Task<bool> DeleteUserCardMonsterSpiritCardAsync(string userId, CardMonsters cardMonsters, SpiritCards SpiritCard);
    Task<bool> DeleteUserCardSpellSpiritCardAsync(string userId, CardSpells cardSpells, SpiritCards SpiritCard);
    Task<SpiritCards> GetUserSpiritCardByIdAsync(string user_id, string Id);
    Task<SpiritCards> SumPowerUserSpiritCardsAsync();
}