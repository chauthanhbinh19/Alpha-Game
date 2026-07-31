using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserEquipmentsService
{
    Task<List<Equipments>> GetAllRankPowerAsync(string userId, List<Equipments> EquipmentsList);
    Task<List<Equipments>> GetUserEquipmentsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<Equipments>> GetUserAllEquipmentsAsync(string userId);
    Task<int> GetUserEquipmentsCountAsync(string userId, string search, string type, string rare);
    Task<Equipments> GetUserEquipmentsByIdAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEquipmentAsync(string userId, Equipments equipment);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserEquipmentsBatchAsync(string userId, List<(Equipments data, double quantity)> list);
    Task<bool> UpdateUserEquipmentLevelAsync(string userId, Equipments equipment);
    Task<bool> UpdateUserEquipmentStarAsync(string userId, Equipments equipment);
    Task UpdateUserCurrencyAsync(string userId, string Id, double amount);
    Task InsertUserCardHeroEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertUserCardCaptainEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertUserCardColonelEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertUserCardGeneralEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertUserCardAdmiralEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertUserCardMonsterEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertUserCardMilitaryEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertUserCardSpellEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertUserBookEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertUserPetEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertUserCardSoldierEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task<List<Equipments>> GetUserCardHeroesEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetUserCardCaptainsEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetUserCardColonelsEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetUserCardGeneralsEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetUserCardAdmiralsEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetUserCardMonstersEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetUserCardMilitariesEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetUserCardSpellsEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetUserBooksEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetUserPetsEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetUserCardSoldiersEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetAllUserCardHeroesEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardCaptainsEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardColonelsEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardGeneralsEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardAdmiralsEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardMonstersEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardMilitariesEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardSpellsEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserBooksEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserPetsEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardSoldiersEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<Equipments> GetAllUserEquipmentsByCardHeorIdAsync(string userId, string cardHeroId);
    Task<Equipments> GetAllUserEquipmentsByCardCaptainIdAsync(string userId, string cardCaptainId);
    Task<Equipments> GetAllUserEquipmentsByCardColonelIdAsync(string userId, string cardColonelId);
    Task<Equipments> GetAllUserEquipmentsByCardGeneralIdAsync(string userId, string cardGeneralId);
    Task<Equipments> GetAllUserEquipmentsByCardAdmiralIdAsync(string userId, string cardAdmiralId);
    Task<Equipments> GetAllUserEquipmentsByCardMonsterIdAsync(string userId, string cardMonsterId);
    Task<Equipments> GetAllUserEquipmentsByCardMilitaryIdAsync(string userId, string cardMilitaryId);
    Task<Equipments> GetAllUserEquipmentsByCardSpellIdAsync(string userId, string cardSpellId);
    Task<Equipments> GetAllUserEquipmentsByBookIdAsync(string userId, string bookId);
    Task<Equipments> GetAllUserEquipmentsByPetIdAsync(string userId, string petId);
    Task<Equipments> GetAllUserEquipmentsByCardSoldierIdAsync(string userId, string cardSoldierId);
    // Cho CardHero
    Task<bool> EquipAllEquipmentsOfTypeToCardHeroAsync(string userId, string cardHeroId, string type);
    Task<bool> EquipAllEquipmentsToCardHeroAsync(string userId, string cardHeroId);

    // Cho CardCaptain
    Task<bool> EquipAllEquipmentsOfTypeToCardCaptainAsync(string userId, string cardCaptainId, string type);
    Task<bool> EquipAllEquipmentsToCardCaptainAsync(string userId, string cardCaptainId);

    // Cho CardColonel
    Task<bool> EquipAllEquipmentsOfTypeToCardColonelAsync(string userId, string cardColonelId, string type);
    Task<bool> EquipAllEquipmentsToCardColonelAsync(string userId, string cardColonelId);

    // Cho CardGeneral
    Task<bool> EquipAllEquipmentsOfTypeToCardGeneralAsync(string userId, string cardGeneralId, string type);
    Task<bool> EquipAllEquipmentsToCardGeneralAsync(string userId, string cardGeneralId);

    // Cho CardAdmiral
    Task<bool> EquipAllEquipmentsOfTypeToCardAdmiralAsync(string userId, string cardAdmiralId, string type);
    Task<bool> EquipAllEquipmentsToCardAdmiralAsync(string userId, string cardAdmiralId);

    // Cho CardMonster
    Task<bool> EquipAllEquipmentsOfTypeToCardMonsterAsync(string userId, string cardMonsterId, string type);
    Task<bool> EquipAllEquipmentsToCardMonsterAsync(string userId, string cardMonsterId);

    // Cho CardMilitary
    Task<bool> EquipAllEquipmentsOfTypeToCardMilitaryAsync(string userId, string cardMilitaryId, string type);
    Task<bool> EquipAllEquipmentsToCardMilitaryAsync(string userId, string cardMilitaryId);

    // Cho CardSpell
    Task<bool> EquipAllEquipmentsOfTypeToCardSpellAsync(string userId, string cardSpellId, string type);
    Task<bool> EquipAllEquipmentsToCardSpellAsync(string userId, string cardSpellId);

    // Cho Book
    Task<bool> EquipAllEquipmentsOfTypeToBookAsync(string userId, string bookId, string type);
    Task<bool> EquipAllEquipmentsToBookAsync(string userId, string bookId);

    // Cho Pet
    Task<bool> EquipAllEquipmentsOfTypeToPetAsync(string userId, string petId, string type);
    Task<bool> EquipAllEquipmentsToPetAsync(string userId, string petId);

    Task<bool> EquipAllEquipmentsOfTypeToCardSoldierAsync(string userId, string cardSoldierId, string type);
    Task<bool> EquipAllEquipmentsToCardSoldierAsync(string userId, string cardSoldierId);
}