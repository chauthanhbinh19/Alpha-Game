using System.Collections.Generic;

public interface IUserSpiritBeastService
{
    SpiritBeast GetNewLevelPower(SpiritBeast c, double coefficient);
    SpiritBeast GetNewBreakthroughPower(SpiritBeast c, double coefficient);
    List<SpiritBeast> GetUserSpiritBeast(string user_id, int pageSize, int offset, string rare);
    List<SpiritBeast> GetAllUserSpiritBeast(string user_id, int pageSize, int offset);
    int GetUserSpiritBeastCount(string user_id, string rare);
    bool InsertUserSpiritBeast(SpiritBeast SpiritBeast);
    bool UpdateSpiritBeastLevel(SpiritBeast SpiritBeast, int cardLevel);
    bool UpdateSpiritBeastBreakthrough(SpiritBeast SpiritBeast, int star, int quantity);
    SpiritBeast GetUserCardHeroesSpiritBeast(string userId, CardHeroes cardHeroes);
    SpiritBeast GetUserCardCaptainsSpiritBeast(string userId, CardCaptains cardCaptains);
    SpiritBeast GetUserCardColonelsSpiritBeast(string userId, CardColonels cardColonels);
    SpiritBeast GetUserCardGeneralsSpiritBeast(string userId, CardGenerals cardGenerals);
    SpiritBeast GetUserCardAdmiralsSpiritBeast(string userId, CardAdmirals cardAdmirals);
    SpiritBeast GetUserCardMilitarySpiritBeast(string userId, CardMilitary cardMilitary);
    SpiritBeast GetUserCardMonstersSpiritBeast(string userId, CardMonsters cardMonsters);
    bool InsertOrUpdateUserCardHeroesSpiritBeast(string userId, CardHeroes cardHeroes, SpiritBeast spiritBeast);
    bool InsertOrUpdateUserCardCaptainsSpiritBeast(string userId, CardCaptains cardCaptains, SpiritBeast spiritBeast);
    bool InsertOrUpdateUserCardColonelsSpiritBeast(string userId, CardColonels cardColonels, SpiritBeast spiritBeast);
    bool InsertOrUpdateUserCardGeneralsSpiritBeast(string userId, CardGenerals cardGenerals, SpiritBeast spiritBeast);
    bool InsertOrUpdateUserCardAdmiralsSpiritBeast(string userId, CardAdmirals cardAdmirals, SpiritBeast spiritBeast);
    bool InsertOrUpdateUserCardMilitarySpiritBeast(string userId, CardMilitary cardMilitary, SpiritBeast spiritBeast);
    bool InsertOrUpdateUserCardMonstersSpiritBeast(string userId, CardMonsters cardMonsters, SpiritBeast spiritBeast);
    List<SpiritBeast> GetAllUserCardHeroesSpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeast> GetAllUserCardCaptainsSpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeast> GetAllUserCardColonelsSpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeast> GetAllUserCardGeneralsSpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeast> GetAllUserCardAdmiralsSpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeast> GetAllUserCardMilitarySpiritBeast(string user_id, int pageSize, int offset, string status);
    List<SpiritBeast> GetAllUserCardMonstersSpiritBeast(string user_id, int pageSize, int offset, string status);
    bool DeleteUserCardHeroesSpiritBeast(string userId, CardHeroes cardHeroes, SpiritBeast spiritBeast);
    bool DeleteUserCardCaptainsSpiritBeast(string userId, CardCaptains cardCaptains, SpiritBeast spiritBeast);
    bool DeleteUserCardColonelsSpiritBeast(string userId, CardColonels cardColonels, SpiritBeast spiritBeast);
    bool DeleteUserCardGeneralsSpiritBeast(string userId, CardGenerals cardGenerals, SpiritBeast spiritBeast);
    bool DeleteUserCardAdmiralsSpiritBeast(string userId, CardAdmirals cardAdmirals, SpiritBeast spiritBeast);
    bool DeleteUserCardMilitarySpiritBeast(string userId, CardMilitary cardMilitary, SpiritBeast spiritBeast);
    bool DeleteUserCardMonstersSpiritBeast(string userId, CardMonsters cardMonsters, SpiritBeast spiritBeast);
    SpiritBeast GetUserSpiritBeastById(string user_id, string Id);
    SpiritBeast SumPowerUserSpiritBeast();
}