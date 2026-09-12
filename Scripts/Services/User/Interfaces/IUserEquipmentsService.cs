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
    Task<InsertOrUpdateResult<bool>> UpdateUserEquipmentLevelAsync(string userId, Equipments equipment);
    Task<InsertOrUpdateResult<bool>> UpdateUserEquipmentStarAsync(string userId, Equipments equipment);
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
    Task<List<Equipments>> GetUserCardHeroesEquipmentsAsync(string userId, List<string> cardIdList, string type);
    Task<List<Equipments>> GetUserCardCaptainsEquipmentsAsync(string userId, List<string> cardIdList, string type);
    Task<List<Equipments>> GetUserCardColonelsEquipmentsAsync(string userId, List<string> cardIdList, string type);
    Task<List<Equipments>> GetUserCardGeneralsEquipmentsAsync(string userId, List<string> cardIdList, string type);
    Task<List<Equipments>> GetUserCardAdmiralsEquipmentsAsync(string userId, List<string> cardIdList, string type);
    Task<List<Equipments>> GetUserCardMonstersEquipmentsAsync(string userId, List<string> cardIdList, string type);
    Task<List<Equipments>> GetUserCardMilitariesEquipmentsAsync(string userId, List<string> cardIdList, string type);
    Task<List<Equipments>> GetUserCardSpellsEquipmentsAsync(string userId, List<string> cardIdList, string type);
    Task<List<Equipments>> GetUserBooksEquipmentsAsync(string userId, List<string> cardIdList, string type);
    Task<List<Equipments>> GetUserPetsEquipmentsAsync(string userId, List<string> cardIdList, string type);
    Task<List<Equipments>> GetUserCardSoldiersEquipmentsAsync(string userId, List<string> cardIdList, string type);
    Task<List<Equipments>> GetAllUserCardHeroesEquipmentsAsync(string userId, string search, string type, string rare, string set, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardCaptainsEquipmentsAsync(string userId, string search, string type, string rare, string set, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardColonelsEquipmentsAsync(string userId, string search, string type, string rare, string set, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardGeneralsEquipmentsAsync(string userId, string search, string type, string rare, string set, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardAdmiralsEquipmentsAsync(string userId, string search, string type, string rare, string set, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardMonstersEquipmentsAsync(string userId, string search, string type, string rare, string set, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardMilitariesEquipmentsAsync(string userId, string search, string type, string rare, string set, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardSpellsEquipmentsAsync(string userId, string search, string type, string rare, string set, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserBooksEquipmentsAsync(string userId, string search, string type, string rare, string set, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserPetsEquipmentsAsync(string userId, string search, string type, string rare, string set, int limit, int offset, string status);
    Task<List<Equipments>> GetAllUserCardSoldiersEquipmentsAsync(string userId, string search, string type, string rare, string set, int limit, int offset, string status);
    Task<int> GetUserCardHeroesEquipmentsCountAsync(string userId, string search, string type, string rare, string set);
    Task<int> GetUserCardCaptainsEquipmentsCountAsync(string userId, string search, string type, string rare, string set);
    Task<int> GetUserCardColonelsEquipmentsCountAsync(string userId, string search, string type, string rare, string set);
    Task<int> GetUserCardGeneralsEquipmentsCountAsync(string userId, string search, string type, string rare, string set);
    Task<int> GetUserCardAdmiralsEquipmentsCountAsync(string userId, string search, string type, string rare, string set);
    Task<int> GetUserCardMonstersEquipmentsCountAsync(string userId, string search, string type, string rare, string set);
    Task<int> GetUserCardMilitariesEquipmentsCountAsync(string userId, string search, string type, string rare, string set);
    Task<int> GetUserCardSoldiersEquipmentsCountAsync(string userId, string search, string type, string rare, string set);
    Task<int> GetUserCardSpellsEquipmentsCountAsync(string userId, string search, string type, string rare, string set);
    Task<int> GetUserBooksEquipmentsCountAsync(string userId, string search, string type, string rare, string set);
    Task<int> GetUserPetsEquipmentsCountAsync(string userId, string search, string type, string rare, string set);
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
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardHeroAsync(string userId, string cardHeroId, string type);
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardHeroAsync(string userId, string cardHeroId);

    // Cho CardCaptain
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardCaptainAsync(string userId, string cardCaptainId, string type);
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardCaptainAsync(string userId, string cardCaptainId);

    // Cho CardColonel
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardColonelAsync(string userId, string cardColonelId, string type);
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardColonelAsync(string userId, string cardColonelId);

    // Cho CardGeneral
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardGeneralAsync(string userId, string cardGeneralId, string type);
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardGeneralAsync(string userId, string cardGeneralId);

    // Cho CardAdmiral
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardAdmiralAsync(string userId, string cardAdmiralId, string type);
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardAdmiralAsync(string userId, string cardAdmiralId);

    // Cho CardMonster
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardMonsterAsync(string userId, string cardMonsterId, string type);
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardMonsterAsync(string userId, string cardMonsterId);

    // Cho CardMilitary
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardMilitaryAsync(string userId, string cardMilitaryId, string type);
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardMilitaryAsync(string userId, string cardMilitaryId);

    // Cho CardSpell
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardSpellAsync(string userId, string cardSpellId, string type);
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardSpellAsync(string userId, string cardSpellId);

    // Cho Book
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToBookAsync(string userId, string bookId, string type);
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToBookAsync(string userId, string bookId);

    // Cho Pet
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToPetAsync(string userId, string petId, string type);
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToPetAsync(string userId, string petId);

    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsOfTypeToCardSoldierAsync(string userId, string cardSoldierId, string type);
    Task<InsertOrUpdateResult<bool>> EquipAllEquipmentsToCardSoldierAsync(string userId, string cardSoldierId);
}