using System.Collections.Generic;
using System.Threading.Tasks;

public class UserGearEquipmentService : IUserGearEquipmentService
{
    private readonly IUserGearEquipmentRepository _userGearEquipmentRepository;

    public UserGearEquipmentService(IUserGearEquipmentRepository repository)
    {
        _userGearEquipmentRepository = repository;
    }

    public static IUserGearEquipmentService Create() => ServiceContainer.GetService<IUserGearEquipmentService>();

    public async Task<List<UserGearEquipment>> GetUserGearEquipmentAsync(string userId)
    {
        return await _userGearEquipmentRepository.GetUserGearEquipmentAsync(userId);
    }

    public async Task<InsertOrUpdateResult<UserGearEquipment>> InsertOrUpdateUserGearEquipmentAsync(string userId, int numberOfGear)
    {
        return await _userGearEquipmentRepository.InsertOrUpdateUserGearEquipmentAsync(userId, numberOfGear);
    }

    public async Task<InsertOrUpdateResult<UserGearEquipmentDetail>> InsertOrUpdateUserGearEquipmentDetailAsync(string userId, int numberOfGear, string equipmentId, int position)
    {
        return await _userGearEquipmentRepository.InsertOrUpdateUserGearEquipmentDetailAsync(userId, numberOfGear, equipmentId, position);
    }

    /// <summary>
    /// Hàm điều hướng chung xử lý Upsert Card Gear dựa vào loại thẻ (cardType)
    /// </summary>
    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateCardGearEquipmentAsync(string cardType, string userId, string cardId, int numberOfGear)
    {
        if (string.IsNullOrWhiteSpace(cardType))
        {
            return InsertOrUpdateResult<bool>.Failure("Loại thẻ không được để trống.");
        }

        string normalizedType = cardType.Trim().ToLower();

        return normalizedType switch
        {
            "hero" or "heroes" => await _userGearEquipmentRepository.InsertOrUpdateHeroGearEquipmentAsync(userId, cardId, numberOfGear),
            "captain" or "captains" => await _userGearEquipmentRepository.InsertOrUpdateCaptainGearEquipmentAsync(userId, cardId, numberOfGear),
            "colonel" or "colonels" => await _userGearEquipmentRepository.InsertOrUpdateColonelGearEquipmentAsync(userId, cardId, numberOfGear),
            "general" or "generals" => await _userGearEquipmentRepository.InsertOrUpdateGeneralGearEquipmentAsync(userId, cardId, numberOfGear),
            "admiral" or "admirals" => await _userGearEquipmentRepository.InsertOrUpdateAdmiralGearEquipmentAsync(userId, cardId, numberOfGear),
            "monster" or "monsters" => await _userGearEquipmentRepository.InsertOrUpdateMonsterGearEquipmentAsync(userId, cardId, numberOfGear),
            "military" or "militaries" => await _userGearEquipmentRepository.InsertOrUpdateMilitaryGearEquipmentAsync(userId, cardId, numberOfGear),
            "soldier" or "soldiers" => await _userGearEquipmentRepository.InsertOrUpdateSoldierGearEquipmentAsync(userId, cardId, numberOfGear),
            "spell" or "spells" => await _userGearEquipmentRepository.InsertOrUpdateSpellGearEquipmentAsync(userId, cardId, numberOfGear),
            _ => InsertOrUpdateResult<bool>.Failure($"Loại cardType '{cardType}' không hợp lệ hoặc chưa được hỗ trợ.")
        };
    }
}