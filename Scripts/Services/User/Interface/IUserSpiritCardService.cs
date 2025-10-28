using System.Collections.Generic;

public interface IUserSpiritCardService
{
    SpiritCards GetNewLevelPower(SpiritCards c, double coefficient);
    SpiritCards GetNewBreakthroughPower(SpiritCards c, double coefficient);
    List<SpiritCards> GetUserSpiritCard(string user_id, string type, int pageSize, int offset, string rare);
    List<SpiritCards> GetAllUserSpiritCard(string user_id, int pageSize, int offset);
    int GetUserSpiritCardCount(string user_id, string type, string rare);
    bool InsertUserSpiritCard(SpiritCards SpiritCard);
    bool UpdateSpiritCardLevel(SpiritCards SpiritCard, int cardLevel);
    bool UpdateSpiritCardBreakthrough(SpiritCards SpiritCard, int star, double quantity);
    SpiritCards GetUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes);
    SpiritCards GetUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains);
    SpiritCards GetUserCardColonelsSpiritCard(string userId, CardColonels cardColonels);
    SpiritCards GetUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals);
    SpiritCards GetUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals);
    SpiritCards GetUserCardMilitarySpiritCard(string userId, CardMilitaries cardMilitary);
    SpiritCards GetUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters);
    SpiritCards GetUserCardSpellSpiritCard(string userId, CardSpells cardSpell);
    bool InsertOrUpdateUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes, SpiritCards spiritBeast);
    bool InsertOrUpdateUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains, SpiritCards spiritBeast);
    bool InsertOrUpdateUserCardColonelsSpiritCard(string userId, CardColonels cardColonels, SpiritCards spiritBeast);
    bool InsertOrUpdateUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals, SpiritCards spiritBeast);
    bool InsertOrUpdateUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals, SpiritCards spiritBeast);
    bool InsertOrUpdateUserCardMilitarySpiritCard(string userId, CardMilitaries cardMilitary, SpiritCards spiritBeast);
    bool InsertOrUpdateUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters, SpiritCards spiritBeast);
    bool InsertOrUpdateUserCardSpellSpiritCard(string userId, CardSpells cardSpell, SpiritCards spiritBeast);
    List<SpiritCards> GetAllUserCardHeroesSpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCards> GetAllUserCardCaptainsSpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCards> GetAllUserCardColonelsSpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCards> GetAllUserCardGeneralsSpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCards> GetAllUserCardAdmiralsSpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCards> GetAllUserCardMilitarySpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCards> GetAllUserCardMonstersSpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCards> GetAllUserCardSpellSpiritCard(string user_id, int pageSize, int offset, string status);
    bool DeleteUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes, SpiritCards spiritBeast);
    bool DeleteUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains, SpiritCards spiritBeast);
    bool DeleteUserCardColonelsSpiritCard(string userId, CardColonels cardColonels, SpiritCards spiritBeast);
    bool DeleteUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals, SpiritCards spiritBeast);
    bool DeleteUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals, SpiritCards spiritBeast);
    bool DeleteUserCardMilitarySpiritCard(string userId, CardMilitaries cardMilitary, SpiritCards spiritBeast);
    bool DeleteUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters, SpiritCards spiritBeast);
    bool DeleteUserCardSpellSpiritCard(string userId, CardSpells cardSpell, SpiritCards spiritBeast);
    SpiritCards GetUserSpiritCardById(string user_id, string Id);
    SpiritCards SumPowerUserSpiritCard();
}