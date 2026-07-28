using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCollaborationEquipmentsService : IUserCollaborationEquipmentsService
{
    private static UserCollaborationEquipmentsService _instance;
    private readonly IUserCollaborationEquipmentsRepository _userCollaborationEquipmentsRepository;

    public UserCollaborationEquipmentsService(IUserCollaborationEquipmentsRepository userCollaborationEquipmentsRepository)
    {
        _userCollaborationEquipmentsRepository = userCollaborationEquipmentsRepository;
    }

    public static UserCollaborationEquipmentsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCollaborationEquipmentsService(new UserCollaborationEquipmentsRepository());
        }
        return _instance;
    }

    public async Task<List<CollaborationEquipments>> GetUserCollaborationEquipmentsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> list = await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCollaborationEquipmentsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentsCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserCollaborationEquipmentAsync(CollaborationEquipments collaborationEquipment, string userId)
    {
        var result = await _userCollaborationEquipmentsRepository.InsertUserCollaborationEquipmentAsync(collaborationEquipment, userId);
        if (result)
        {
            await CollaborationEquipmentsGalleryService.Create().InsertCollaborationEquipmentGalleryAsync(userId, collaborationEquipment.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserCollaborationEquipmentLevelAsync(string userId, CollaborationEquipments collaborationEquipment)
    {
        return await _userCollaborationEquipmentsRepository.UpdateUserCollaborationEquipmentLevelAsync(userId, collaborationEquipment);
    }

    public async Task<bool> UpdateUserCollaborationEquipmentStarAsync(string userId, CollaborationEquipments collaborationEquipment)
    {
        var result = await _userCollaborationEquipmentsRepository.UpdateUserCollaborationEquipmentStarAsync(userId, collaborationEquipment);
        if (result)
        {
            await CollaborationEquipmentsGalleryService.Create().UpdateStarCollaborationEquipmentGalleryAsync(userId, collaborationEquipment.Id, collaborationEquipment.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserCollaborationEquipmentBreakthroughAsync(string userId, CollaborationEquipments collaborationEquipment, int star, double quantity)
    {
        return await _userCollaborationEquipmentsRepository.UpdateUserCollaborationEquipmentBreakthroughAsync(userId, collaborationEquipment, star, quantity);
    }

    public async Task<CollaborationEquipments> GetUserCollaborationEquipmentByIdAsync(string userId, string Id)
    {
        return await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentByIdAsync(userId, Id);
    }

    public async Task<CollaborationEquipments> SumPowerUserCollaborationEquipmentsAsync(string userId)
    {
        return await _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserCollaborationEquipmentsBatchAsync(string userId, List<CollaborationEquipments> collaborationEquipments)
    {
        return await _userCollaborationEquipmentsRepository.InsertOrUpdateUserCollaborationEquipmentsBatchAsync(userId, collaborationEquipments);
    }
}
