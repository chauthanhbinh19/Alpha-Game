using System.Collections.Generic;

public interface IUserSpiritBeastService
{
    SpiritBeasts GetNewLevelPower(SpiritBeasts c, double coefficient);
    SpiritBeasts GetNewBreakthroughPower(SpiritBeasts c, double coefficient);
    List<SpiritBeasts> GetUserSpiritBeast(string user_id, int pageSize, int offset, string rare);
    List<SpiritBeasts> GetAllUserSpiritBeast(string user_id, int pageSize, int offset);
    int GetUserSpiritBeastCount(string user_id, string rare);
    bool InsertUserSpiritBeast(SpiritBeasts SpiritBeast);
    bool UpdateSpiritBeastLevel(SpiritBeasts SpiritBeast, int cardLevel);
    bool UpdateSpiritBeastBreakthrough(SpiritBeasts SpiritBeast, int star, double quantity);
    SpiritBeasts GetUserCardHeroesSpiritBeast(string userId, CardHeroes cardHeroes);
    SpiritBeasts GetUserCardCaptainsSpiritBeast(string userId, CardCaptains cardCaptains);
    SpiritBeasts GetUserCardColonelsSpiritBeast(string userId, CardColonels cardColonels);
    SpiritBeasts GetUserCardGeneralsSpiritBeast(string userId, CardGenerals cardGenerals);
    SpiritBeasts GetUserCardAdmiralsSpiritBeast(string userId, CardAdmirals cardAdmirals);
    SpiritBeasts GetUserCardMilitarySpiritBeast(string userId, CardMilitaries cardMilitary);
    SpiritBeasts GetUserCardMonstersSpiritBeast(string userId, CardMonsters cardMonsters);
    SpiritBeasts GetUserCardSpellSpiritBeast(string userId, CardSpells cardSpell);
    bool InsertOrUpdateUserCardHeroesSpiritBeast(string userId, CardHeroes cardHeroes, SpiritBeasts spiritBeast);
    bool InsertOrUpdateUserCardCaptainsSpiritBeast(string userId, CardCaptains cardCaptains, SpiritBeasts spiritBeast);
    bool InsertOrUpdateUserCardColonelsSpiritBeast(string userId, CardColonels cardColonels, SpiritBeasts spiritBeast);
    bool InsertOrUpdateUserCardGeneralsSpiritBeast(string userId, CardGenerals cardGenerals, SpiritBeasts spiritBeast);
    bool InsertOrUpdateUserCardAdmiralsSpiritBeast(string userId, CardAdmirals cardAdmirals, SpiritBeasts spiritBeast);
    bool InsertOrUpdateUserCardMilitarySpiritBeast(string userId, CardMilitaries cardMilitary, SpiritBeasts spiritBeast);
    bool InsertOrUpdateUserCardMonstersSpiritBeast(string userId, CardMonsters cardMonsters, SpiritBeasts spiritBeast);
    bool InsertOrUpdateUserCardSpellSpiritBeast(string userId, CardSpells cardSpell, SpiritBeasts spiritBeast);
    List<SpiritBeasts> GetAllUserCardHeroesSpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeasts> GetAllUserCardCaptainsSpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeasts> GetAllUserCardColonelsSpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeasts> GetAllUserCardGeneralsSpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeasts> GetAllUserCardAdmiralsSpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeasts> GetAllUserCardMilitarySpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeasts> GetAllUserCardMonstersSpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeasts> GetAllUserCardSpellSpiritBeast(string user_id, int pageSize, int offset, string status);
    bool DeleteUserCardHeroesSpiritBeast(string userId, CardHeroes cardHeroes, SpiritBeasts spiritBeast);
    bool DeleteUserCardCaptainsSpiritBeast(string userId, CardCaptains cardCaptains, SpiritBeasts spiritBeast);
    bool DeleteUserCardColonelsSpiritBeast(string userId, CardColonels cardColonels, SpiritBeasts spiritBeast);
    bool DeleteUserCardGeneralsSpiritBeast(string userId, CardGenerals cardGenerals, SpiritBeasts spiritBeast);
    bool DeleteUserCardAdmiralsSpiritBeast(string userId, CardAdmirals cardAdmirals, SpiritBeasts spiritBeast);
    bool DeleteUserCardMilitarySpiritBeast(string userId, CardMilitaries cardMilitary, SpiritBeasts spiritBeast);
    bool DeleteUserCardMonstersSpiritBeast(string userId, CardMonsters cardMonsters, SpiritBeasts spiritBeast);
    bool DeleteUserCardSpellSpiritBeast(string userId, CardSpells cardSpell, SpiritBeasts spiritBeast);
    SpiritBeasts GetUserSpiritBeastById(string user_id, string Id);
    SpiritBeasts SumPowerUserSpiritBeast();
}