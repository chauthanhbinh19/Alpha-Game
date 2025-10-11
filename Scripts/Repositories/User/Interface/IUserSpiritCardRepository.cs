using System.Collections.Generic;

public interface IUserSpiritCardRepository
{
    List<SpiritCard> GetUserSpiritCard(string user_id, string type, int pageSize, int offset, string rare);
    List<SpiritCard> GetAllUserSpiritCard(string user_id, int pageSize, int offset);
    int GetUserSpiritCardCount(string user_id, string type, string rare);
    bool InsertUserSpiritCard(SpiritCard SpiritCard);
    bool UpdateSpiritCardLevel(SpiritCard SpiritCard, int cardLevel);
    bool UpdateSpiritCardBreakthrough(SpiritCard SpiritCard, int star, int quantity);
    SpiritCard GetUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes);
    SpiritCard GetUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains);
    SpiritCard GetUserCardColonelsSpiritCard(string userId, CardColonels cardColonels);
    SpiritCard GetUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals);
    SpiritCard GetUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals);
    SpiritCard GetUserCardMilitarySpiritCard(string userId, CardMilitary cardMilitary);
    SpiritCard GetUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters);
    SpiritCard GetUserCardSpellSpiritCard(string userId, CardSpell cardSpell);
    bool InsertOrUpdateUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes, SpiritCard spiritBeast);
    bool InsertOrUpdateUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains, SpiritCard spiritBeast);
    bool InsertOrUpdateUserCardColonelsSpiritCard(string userId, CardColonels cardColonels, SpiritCard spiritBeast);
    bool InsertOrUpdateUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals, SpiritCard spiritBeast);
    bool InsertOrUpdateUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals, SpiritCard spiritBeast);
    bool InsertOrUpdateUserCardMilitarySpiritCard(string userId, CardMilitary cardMilitary, SpiritCard spiritBeast);
    bool InsertOrUpdateUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters, SpiritCard spiritBeast);
    bool InsertOrUpdateUserCardSpellSpiritCard(string userId, CardSpell cardSpell, SpiritCard spiritBeast);
    List<SpiritCard> GetAllUserCardHeroesSpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCard> GetAllUserCardCaptainsSpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCard> GetAllUserCardColonelsSpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCard> GetAllUserCardGeneralsSpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCard> GetAllUserCardAdmiralsSpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCard> GetAllUserCardMilitarySpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCard> GetAllUserCardMonstersSpiritCard(string user_id, int pageSize, int offset, string status);
    List<SpiritCard> GetAllUserCardSpellSpiritCard(string user_id, int pageSize, int offset, string status);
    bool DeleteUserCardHeroesSpiritCard(string userId, CardHeroes cardHeroes, SpiritCard spiritBeast);
    bool DeleteUserCardCaptainsSpiritCard(string userId, CardCaptains cardCaptains, SpiritCard spiritBeast);
    bool DeleteUserCardColonelsSpiritCard(string userId, CardColonels cardColonels, SpiritCard spiritBeast);
    bool DeleteUserCardGeneralsSpiritCard(string userId, CardGenerals cardGenerals, SpiritCard spiritBeast);
    bool DeleteUserCardAdmiralsSpiritCard(string userId, CardAdmirals cardAdmirals, SpiritCard spiritBeast);
    bool DeleteUserCardMilitarySpiritCard(string userId, CardMilitary cardMilitary, SpiritCard spiritBeast);
    bool DeleteUserCardMonstersSpiritCard(string userId, CardMonsters cardMonsters, SpiritCard spiritBeast);
    bool DeleteUserCardSpellSpiritCard(string userId, CardSpell cardSpell, SpiritCard spiritBeast);
    SpiritCard GetUserSpiritCardById(string user_id, string Id);
    SpiritCard SumPowerUserSpiritCard();
}