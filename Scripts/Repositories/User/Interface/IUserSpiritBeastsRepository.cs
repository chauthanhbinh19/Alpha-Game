using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSpiritBeastsRepository
{
    Task<List<SpiritBeasts>> GetUserSpiritBeastsAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<List<SpiritBeasts>> GetAllUserSpiritBeastsAsync(string user_id, int pageSize, int offset);
    Task<int> GetUserSpiritBeastsCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserSpiritBeastAsync(SpiritBeasts SpiritBeast);
    Task<bool> UpdateSpiritBeastLevelAsync(SpiritBeasts SpiritBeast, int cardLevel);
    Task<bool> UpdateSpiritBeastBreakthroughAsync(SpiritBeasts SpiritBeast, int star, double quantity);
    Task<SpiritBeasts> GetUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHeroes);
    Task<SpiritBeasts> GetUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptains);
    Task<SpiritBeasts> GetUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonels);
    Task<SpiritBeasts> GetUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGenerals);
    Task<SpiritBeasts> GetUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmirals);
    Task<SpiritBeasts> GetUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitary);
    Task<SpiritBeasts> GetUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonsters);
    Task<SpiritBeasts> GetUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpell);
    Task<bool> InsertOrUpdateUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHeroes, SpiritBeasts spiritBeast);
    Task<bool> InsertOrUpdateUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptains, SpiritBeasts spiritBeast);
    Task<bool> InsertOrUpdateUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonels, SpiritBeasts spiritBeast);
    Task<bool> InsertOrUpdateUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGenerals, SpiritBeasts spiritBeast);
    Task<bool> InsertOrUpdateUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmirals, SpiritBeasts spiritBeast);
    Task<bool> InsertOrUpdateUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitary, SpiritBeasts spiritBeast);
    Task<bool> InsertOrUpdateUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonsters, SpiritBeasts spiritBeast);
    Task<bool> InsertOrUpdateUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpell, SpiritBeasts spiritBeast);
    Task<List<SpiritBeasts>> GetAllUserCardHeroesSpiritBeastAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardCaptainsSpiritBeastAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardColonelsSpiritBeastAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardGeneralsSpiritBeastAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardAdmiralsSpiritBeastAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardMilitariesSpiritBeastAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardMonstersSpiritBeastAsync(string user_id, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardSpellsSpiritBeastAsync(string user_id, int pageSize, int offset, string status);
    Task<bool> DeleteUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHeroes, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptains, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonels, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGenerals, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmirals, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitaries, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonsters, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpells, SpiritBeasts spiritBeast);
    Task<SpiritBeasts> GetUserSpiritBeastByIdAsync(string user_id, string Id);
    Task<SpiritBeasts> SumPowerUserSpiritBeastsAsync();
}