using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSpiritBeastsService
{
    Task<List<SpiritBeasts>> GetUserSpiritBeastsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<List<SpiritBeasts>> GetAllUserSpiritBeastsAsync(string userId, int pageSize, int offset);
    Task<List<SpiritBeasts>> GetSpiritBeastsByCardIdsAsync(string userId, List<string> cardIds);
    Task<int> GetUserSpiritBeastsCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserSpiritBeastAsync(string userId, SpiritBeasts spiritBeast);
    Task<bool> InsertOrUpdateUserSpiritBeastsBatchAsync(string userId, List<SpiritBeasts> spiritBeasts);
    Task<bool> UpdateUserSpiritBeastLevelAsync(string userId, SpiritBeasts spiritBeast);
    Task<bool> UpdateUserSpiritBeastStarAsync(string userId, SpiritBeasts spiritBeast);
    Task<bool> UpdateUserSpiritBeastBreakthroughAsync(string userId, SpiritBeasts spiritBeast, int star, double quantity);
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
    Task<List<SpiritBeasts>> GetAllUserCardHeroesSpiritBeastAsync(string userId, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardCaptainsSpiritBeastAsync(string userId, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardColonelsSpiritBeastAsync(string userId, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardGeneralsSpiritBeastAsync(string userId, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardAdmiralsSpiritBeastAsync(string userId, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardMilitariesSpiritBeastAsync(string userId, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardMonstersSpiritBeastAsync(string userId, int pageSize, int offset, string status);
    Task<List<SpiritBeasts>> GetAllUserCardSpellsSpiritBeastAsync(string userId, int pageSize, int offset, string status);
    Task<bool> DeleteUserCardHeroSpiritBeastAsync(string userId, CardHeroes cardHeroes, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardCaptainSpiritBeastAsync(string userId, CardCaptains cardCaptains, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardColonelSpiritBeastAsync(string userId, CardColonels cardColonels, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardGeneralSpiritBeastAsync(string userId, CardGenerals cardGenerals, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardAdmiralSpiritBeastAsync(string userId, CardAdmirals cardAdmirals, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardMilitarySpiritBeastAsync(string userId, CardMilitaries cardMilitaries, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardMonsterSpiritBeastAsync(string userId, CardMonsters cardMonsters, SpiritBeasts spiritBeast);
    Task<bool> DeleteUserCardSpellSpiritBeastAsync(string userId, CardSpells cardSpells, SpiritBeasts spiritBeast);
    Task<SpiritBeasts> GetUserSpiritBeastByIdAsync(string userId, string Id);
    Task<SpiritBeasts> SumPowerUserSpiritBeastsAsync(string userId);
}