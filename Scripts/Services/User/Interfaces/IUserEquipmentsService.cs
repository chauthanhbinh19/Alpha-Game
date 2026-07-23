using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserEquipmentsService
{
    Task<List<Equipments>> GetAllRankPowerAsync(string userId, List<Equipments> EquipmentsList);
    Task<List<Equipments>> GetUserEquipmentsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<List<Equipments>> GetUserAllEquipmentsAsync(string userId);
    Task<int> GetUserEquipmentsCountAsync(string userId, string search, string type, string rare);
    Task<Equipments> GetUserEquipmentsByIdAsync(string userId, string Id);
    Task<bool> InsertUserEquipmentAsync(string userId, string Id, double quantity);
    Task<bool> InsertOrUpdateUserEquipmentsBatchAsync(string userId, List<(Equipments data, double quantity)> list);
    Task<bool> UpdateUserEquipmentLevelAsync(string userId, Equipments equipment);
    Task<bool> UpdateUserEquipmentStarAsync(string userId, Equipments equipment);
    Task<bool> UpdateUserEquipmentsBreakthroughAsync(string userId, Equipments equipment, int star, double quantity);
    Task UpdateUserCurrencyAsync(string userId, string Id, double amount);
    Task InsertCardHeroEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertCardCaptainEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertCardColonelEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertCardGeneralEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertCardAdmiralEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertCardMonsterEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertCardMilitaryEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertCardSpellEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertBookEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertPetEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task InsertCardSoldierEquipmentsAsync(string userId, string Id, Equipments equipment, int position);
    Task<List<Equipments>> GetCardHeroesEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetCardCaptainsEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetCardColonelsEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetCardGeneralsEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetCardAdmiralsEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetCardMonstersEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetCardMilitariesEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetCardSpellsEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetBooksEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetPetsEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetCardSoldiersEquipmentsAsync(string userId, string cardId, string type);
    Task<List<Equipments>> GetAllCardHeroesEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardCaptainsEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardColonelsEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardGeneralsEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardAdmiralsEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardMonstersEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardMilitariesEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardSpellsEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllBooksEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllPetsEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardSoldiersEquipmentsAsync(string userId, string type, int limit, int offset, string status);
    Task<Equipments> GetAllEquipmentsByCardHeorIdAsync(string userId, string cardHeroId);
    Task<Equipments> GetAllEquipmentsByCardCaptainIdAsync(string userId, string cardCaptainId);
    Task<Equipments> GetAllEquipmentsByCardColonelIdAsync(string userId, string cardColonelId);
    Task<Equipments> GetAllEquipmentsByCardGeneralIdAsync(string userId, string cardGeneralId);
    Task<Equipments> GetAllEquipmentsByCardAdmiralIdAsync(string userId, string cardAdmiralId);
    Task<Equipments> GetAllEquipmentsByCardMonsterIdAsync(string userId, string cardMonsterId);
    Task<Equipments> GetAllEquipmentsByCardMilitaryIdAsync(string userId, string cardMilitaryId);
    Task<Equipments> GetAllEquipmentsByCardSpellIdAsync(string userId, string cardSpellId);
    Task<Equipments> GetAllEquipmentsByBookIdAsync(string userId, string bookId);
    Task<Equipments> GetAllEquipmentsByPetIdAsync(string userId, string petId);
    Task<Equipments> GetAllEquipmentsByCardSoldierIdAsync(string userId, string cardSoldierId);
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