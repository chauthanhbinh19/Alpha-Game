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

    public async Task<List<CollaborationEquipments>> GetUserCollaborationEquipmentsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> list = await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCollaborationEquipmentsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserCollaborationEquipmentAsync(CollaborationEquipments collaborationEquipment, string userId)
    {
        return await _userCollaborationEquipmentsRepository.InsertUserCollaborationEquipmentAsync(collaborationEquipment, userId);
    }

    public async Task<bool> UpdateCollaborationEquipmentLevelAsync(CollaborationEquipments collaborationEquipment, int level)
    {
        return await _userCollaborationEquipmentsRepository.UpdateCollaborationEquipmentLevelAsync(collaborationEquipment, level);
    }

    public async Task<bool> UpdateCollaborationEquipmentBreakthroughAsync(CollaborationEquipments collaborationEquipment, int star, double quantity)
    {
        return await _userCollaborationEquipmentsRepository.UpdateCollaborationEquipmentBreakthroughAsync(collaborationEquipment, star, quantity);
    }

    public async Task<CollaborationEquipments> GetUserCollaborationEquipmentByIdAsync(string user_id, string Id)
    {
        return await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentByIdAsync(user_id, Id);
    }

    public async Task<CollaborationEquipments> SumPowerUserCollaborationEquipmentsAsync()
    {
        return await _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync();
    }

    public async Task<bool> InsertOrUpdateUserCollaborationEquipmentsBatchAsync(List<CollaborationEquipments> collaborationEquipments)
    {
        return await _userCollaborationEquipmentsRepository.InsertOrUpdateUserCollaborationEquipmentsBatchAsync(collaborationEquipments);
    }
}
