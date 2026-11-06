using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserGearEquipmentRepository
{
    Task<List<UserGearEquipment>> GetUserGearEquipmentAsync(string userId);
    
    Task<InsertOrUpdateResult<UserGearEquipment>> InsertOrUpdateUserGearEquipmentAsync(string userId, int numberOfGear);
    
    Task<InsertOrUpdateResult<UserGearEquipmentDetail>> InsertOrUpdateUserGearEquipmentDetailAsync(string userId, int numberOfGear, string equipmentId, int position);

    // Các hàm InsertOrUpdate cho từng loại Card Gear Equipment
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateHeroGearEquipmentAsync(string userId, string cardHeroId, int numberOfGear);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateCaptainGearEquipmentAsync(string userId, string cardCaptainId, int numberOfGear);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateColonelGearEquipmentAsync(string userId, string cardColonelId, int numberOfGear);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateGeneralGearEquipmentAsync(string userId, string cardGeneralId, int numberOfGear);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateAdmiralGearEquipmentAsync(string userId, string cardAdmiralId, int numberOfGear);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateMonsterGearEquipmentAsync(string userId, string cardMonsterId, int numberOfGear);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateMilitaryGearEquipmentAsync(string userId, string cardMilitaryId, int numberOfGear);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateSoldierGearEquipmentAsync(string userId, string cardSoldierId, int numberOfGear);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateSpellGearEquipmentAsync(string userId, string cardSpellId, int numberOfGear);
}