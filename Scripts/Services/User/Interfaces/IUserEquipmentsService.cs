using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserEquipmentsService
{
    Task<List<Equipments>> GetAllRankPowerAsync(string user_id, List<Equipments> EquipmentsList);
    Task<List<Equipments>> GetUserEquipmentsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<List<Equipments>> GetUserAllEquipmentsAsync(string user_id);
    Task<int> GetUserEquipmentsCountAsync(string user_id, string search, string type, string rare);
    Task<Equipments> GetUserEquipmentsByIdAsync(string user_id, string Id);
    Task<bool> BuyEquipmentAsync(string Id, double quantity);
    Task<bool> InsertOrUpdateUserEquipmentsBatchAsync(List<(string equipmentId, Equipments data, double quantity)> list);
    Task<bool> UpdateEquipmentsLevelAsync(Equipments equipment, int cardLevel);
    Task<bool> UpdateEquipmentsBreakthroughAsync(Equipments equipment, int star, double quantity);
    Task UpdateUserCurrencyAsync(string Id, double amount);
    Task InsertCardHeroEquipmentsAsync(string Id, Equipments equipment, int position);
    Task InsertCardCaptainEquipmentsAsync(string Id, Equipments equipment, int position);
    Task InsertCardColonelEquipmentsAsync(string Id, Equipments equipment, int position);
    Task InsertCardGeneralEquipmentsAsync(string Id, Equipments equipment, int position);
    Task InsertCardAdmiralEquipmentsAsync(string Id, Equipments equipment, int position);
    Task InsertCardMonsterEquipmentsAsync(string Id, Equipments equipment, int position);
    Task InsertCardMilitaryEquipmentsAsync(string Id, Equipments equipment, int position);
    Task InsertCardSpellEquipmentsAsync(string Id, Equipments equipment, int position);
    Task InsertBookEquipmentsAsync(string Id, Equipments equipment, int position);
    Task InsertPetEquipmentsAsync(string Id, Equipments equipment, int position);
    Task<List<Equipments>> GetCardHeroesEquipmentsAsync(string user_id, string card_id, string type);
    Task<List<Equipments>> GetCardCaptainsEquipmentsAsync(string user_id, string card_id, string type);
    Task<List<Equipments>> GetCardColonelsEquipmentsAsync(string user_id, string card_id, string type);
    Task<List<Equipments>> GetCardGeneralsEquipmentsAsync(string user_id, string card_id, string type);
    Task<List<Equipments>> GetCardAdmiralsEquipmentsAsync(string user_id, string card_id, string type);
    Task<List<Equipments>> GetCardMonstersEquipmentsAsync(string user_id, string card_id, string type);
    Task<List<Equipments>> GetCardMilitariesEquipmentsAsync(string user_id, string card_id, string type);
    Task<List<Equipments>> GetCardSpellsEquipmentsAsync(string user_id, string card_id, string type);
    Task<List<Equipments>> GetBooksEquipmentsAsync(string user_id, string card_id, string type);
    Task<List<Equipments>> GetPetsEquipmentsAsync(string user_id, string card_id, string type);
    Task<List<Equipments>> GetAllCardHeroesEquipmentsAsync(string user_id, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardCaptainsEquipmentsAsync(string user_id, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardColonelsEquipmentsAsync(string user_id, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardGeneralsEquipmentsAsync(string user_id, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardAdmiralsEquipmentsAsync(string user_id, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardMonstersEquipmentsAsync(string user_id, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardMilitariesEquipmentsAsync(string user_id, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllCardSpellsEquipmentsAsync(string user_id, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllBooksEquipmentsAsync(string user_id, string type, int limit, int offset, string status);
    Task<List<Equipments>> GetAllPetsEquipmentsAsync(string user_id, string type, int limit, int offset, string status);
    Task<Equipments> GetAllEquipmentsByCardHeorIdAsync(string user_id, string cardHeroId);
    Task<Equipments> GetAllEquipmentsByCardCaptainIdAsync(string user_id, string cardCaptainId);
    Task<Equipments> GetAllEquipmentsByCardColonelIdAsync(string user_id, string cardColonelId);
    Task<Equipments> GetAllEquipmentsByCardGeneralIdAsync(string user_id, string cardGeneralId);
    Task<Equipments> GetAllEquipmentsByCardAdmiralIdAsync(string user_id, string cardAdmiralId);
    Task<Equipments> GetAllEquipmentsByCardMonsterIdAsync(string user_id, string cardMonsterId);
    Task<Equipments> GetAllEquipmentsByCardMilitaryIdAsync(string user_id, string cardMilitaryId);
    Task<Equipments> GetAllEquipmentsByCardSpellIdAsync(string user_id, string cardSpellId);
    Task<Equipments> GetAllEquipmentsByBookIdAsync(string user_id, string bookId);
    Task<Equipments> GetAllEquipmentsByPetIdAsync(string user_id, string petId);
    // Cho CardHero
    Task<bool> EquipAllEquipmentsOfTypeToCardHeroAsync(string cardHeroId, string type);
    Task<bool> EquipAllEquipmentsToCardHeroAsync(string cardHeroId);

    // Cho CardCaptain
    Task<bool> EquipAllEquipmentsOfTypeToCardCaptainAsync(string cardCaptainId, string type);
    Task<bool> EquipAllEquipmentsToCardCaptainAsync(string cardCaptainId);

    // Cho CardColonel
    Task<bool> EquipAllEquipmentsOfTypeToCardColonelAsync(string cardColonelId, string type);
    Task<bool> EquipAllEquipmentsToCardColonelAsync(string cardColonelId);

    // Cho CardGeneral
    Task<bool> EquipAllEquipmentsOfTypeToCardGeneralAsync(string cardGeneralId, string type);
    Task<bool> EquipAllEquipmentsToCardGeneralAsync(string cardGeneralId);

    // Cho CardAdmiral
    Task<bool> EquipAllEquipmentsOfTypeToCardAdmiralAsync(string cardAdmiralId, string type);
    Task<bool> EquipAllEquipmentsToCardAdmiralAsync(string cardAdmiralId);

    // Cho CardMonster
    Task<bool> EquipAllEquipmentsOfTypeToCardMonsterAsync(string cardMonsterId, string type);
    Task<bool> EquipAllEquipmentsToCardMonsterAsync(string cardMonsterId);

    // Cho CardMilitary
    Task<bool> EquipAllEquipmentsOfTypeToCardMilitaryAsync(string cardMilitaryId, string type);
    Task<bool> EquipAllEquipmentsToCardMilitaryAsync(string cardMilitaryId);

    // Cho CardSpell
    Task<bool> EquipAllEquipmentsOfTypeToCardSpellAsync(string cardSpellId, string type);
    Task<bool> EquipAllEquipmentsToCardSpellAsync(string cardSpellId);

    // Cho Book
    Task<bool> EquipAllEquipmentsOfTypeToBookAsync(string bookId, string type);
    Task<bool> EquipAllEquipmentsToBookAsync(string bookId);

    // Cho Pet
    Task<bool> EquipAllEquipmentsOfTypeToPetAsync(string petId, string type);
    Task<bool> EquipAllEquipmentsToPetAsync(string petId);
}