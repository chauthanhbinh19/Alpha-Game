using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserGearEquipmentService
{
    Task<List<UserGearEquipment>> GetUserGearEquipmentAsync(string userId);
    
    Task<InsertOrUpdateResult<UserGearEquipment>> InsertOrUpdateUserGearEquipmentAsync(string userId, int numberOfGear);
    
    Task<InsertOrUpdateResult<UserGearEquipmentDetail>> InsertOrUpdateUserGearEquipmentDetailAsync(string userId, int numberOfGear, string equipmentId, int position);

    // Các hàm InsertOrUpdate cho từng loại Card Gear Equipment tương ứng
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateCardGearEquipmentAsync(string cardType, string userId, string cardId, int numberOfGear);
}