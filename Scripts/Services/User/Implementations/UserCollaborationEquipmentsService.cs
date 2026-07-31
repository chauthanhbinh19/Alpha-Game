using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCollaborationEquipmentsService : IUserCollaborationEquipmentsService
{
    private readonly IUserCollaborationEquipmentsRepository _userCollaborationEquipmentsRepository;
    private readonly ICollaborationEquipmentsGalleryService _collaborationEquipmentsGalleryService;
    private readonly ICollaborationEquipmentsService _collaborationEquipmentsService;
    private readonly IPowerManagerService _powerManagerService;

    public UserCollaborationEquipmentsService(
        IUserCollaborationEquipmentsRepository userCollaborationEquipmentsRepository,
        ICollaborationEquipmentsGalleryService collaborationEquipmentsGalleryService,
        ICollaborationEquipmentsService collaborationEquipmentsService,
        IPowerManagerService powerManagerService)
    {
        _userCollaborationEquipmentsRepository = userCollaborationEquipmentsRepository;
        _collaborationEquipmentsGalleryService = collaborationEquipmentsGalleryService;
        _collaborationEquipmentsService = collaborationEquipmentsService;
        _powerManagerService = powerManagerService;
    }

    public static IUserCollaborationEquipmentsService Create() => ServiceContainer.GetService<IUserCollaborationEquipmentsService>();

    public async Task<List<CollaborationEquipments>> GetUserCollaborationEquipmentsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> list = await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        list = LevelEvaluatorHelper.GetLevelPower(list);
        list = StarEvaluatorHelper.GetStarPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCollaborationEquipmentsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCollaborationEquipmentAsync(string userId, CollaborationEquipments collaborationEquipment)
    {
        CollaborationEquipments oldCollaborationEquipment = await _collaborationEquipmentsService.SumPowerCollaborationEquipmentsPercentAsync(userId);
        var insertOrUpdateResult = await _userCollaborationEquipmentsRepository.InsertOrUpdateUserCollaborationEquipmentAsync(userId, collaborationEquipment);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        await _collaborationEquipmentsGalleryService.InsertCollaborationEquipmentGalleryAsync(userId, collaborationEquipment.Id);

        CollaborationEquipments newCollaborationEquipment = await _collaborationEquipmentsService.SumPowerCollaborationEquipmentsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newCollaborationEquipment - (PowerManager)oldCollaborationEquipment;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Inserted(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCollaborationEquipmentsBatchAsync(string userId, List<CollaborationEquipments> collaborationEquipmentes)
    {
        CollaborationEquipments oldCollaborationEquipment = await _collaborationEquipmentsService.SumPowerCollaborationEquipmentsPercentAsync(userId);
        var repositoryResult = await _userCollaborationEquipmentsRepository.InsertOrUpdateUserCollaborationEquipmentsBatchAsync(userId, collaborationEquipmentes);

        // 1. Kiểm tra Null hoặc nếu Repository trả về không thành công
        if (repositoryResult?.Data == null || !repositoryResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        // 2. Gộp logic xử lý Gallery nếu có thẻ mới được Insert (dùng cho cả Inserted và Mixed)
        var newlyInsertedCards = repositoryResult.Data.InsertedItems;
        if (newlyInsertedCards != null && newlyInsertedCards.Count > 0)
        {
            await _collaborationEquipmentsGalleryService.InsertBatchCollaborationEquipmentsGalleryAsync(userId, newlyInsertedCards);
        }

        CollaborationEquipments newCollaborationEquipment = await _collaborationEquipmentsService.SumPowerCollaborationEquipmentsPercentAsync(userId);
        PowerManager deltaPower = (PowerManager)newCollaborationEquipment - (PowerManager)oldCollaborationEquipment;

        if (deltaPower.Power == 0)
        {
            return InsertOrUpdateResult<bool>.Inserted(false);
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        // 3. Mapping kết quả OperationType trả về gọn gàng
        return repositoryResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = repositoryResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<bool> UpdateUserCollaborationEquipmentLevelAsync(string userId, CollaborationEquipments collaborationEquipment)
    {
        var updateResult = await _userCollaborationEquipmentsRepository.UpdateUserCollaborationEquipmentLevelAsync(userId, collaborationEquipment);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateUserCollaborationEquipmentStarAsync(string userId, CollaborationEquipments collaborationEquipment)
    {
        var updateResult = await _userCollaborationEquipmentsRepository.UpdateUserCollaborationEquipmentStarAsync(userId, collaborationEquipment);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        await _collaborationEquipmentsGalleryService.UpdateTempStarCollaborationEquipmentGalleryAsync(userId, collaborationEquipment.Id, collaborationEquipment.Star);

        return true;
    }

    public async Task<CollaborationEquipments> GetUserCollaborationEquipmentByIdAsync(string userId, string Id)
    {
        var result = await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentByIdAsync(userId, Id);

        result = QualityEvaluatorHelper.GetQualityPower(result);
        result = LevelEvaluatorHelper.GetLevelPower(result);
        result = StarEvaluatorHelper.GetStarPower(result);

        return result;
    }

    public async Task<CollaborationEquipments> SumPowerUserCollaborationEquipmentsAsync(string userId)
    {
        return await _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync(userId);
    }
}
