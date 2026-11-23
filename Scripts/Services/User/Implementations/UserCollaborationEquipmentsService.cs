using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCollaborationEquipmentsService : IUserCollaborationEquipmentsService
{
    private readonly IUserCollaborationEquipmentsRepository _userCollaborationEquipmentsRepository;
    private readonly ICollaborationEquipmentsGalleryService _collaborationEquipmentsGalleryService;
    private readonly ICollaborationEquipmentsService _collaborationEquipmentsService;

    public UserCollaborationEquipmentsService(
        IUserCollaborationEquipmentsRepository userCollaborationEquipmentsRepository,
        ICollaborationEquipmentsGalleryService collaborationEquipmentsGalleryService,
        ICollaborationEquipmentsService collaborationEquipmentsService)
    {
        _userCollaborationEquipmentsRepository = userCollaborationEquipmentsRepository;
        _collaborationEquipmentsGalleryService = collaborationEquipmentsGalleryService;
        _collaborationEquipmentsService = collaborationEquipmentsService;
    }

    public static IUserCollaborationEquipmentsService Create() => ServiceContainer.GetService<IUserCollaborationEquipmentsService>();

    public async Task<List<CollaborationEquipments>> GetUserCollaborationEquipmentsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> result = await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentsAsync(userId, search, type, pageSize, offset, rare);

        foreach (var item in result)
        {
            item.BaseStats = new BaseStats(item);
        }

        result = PowerEvaluatorHelper.EvaluatePower(result);
        ListSortHelper.SortByPower(result);
        return result;
    }

    public async Task<int> GetUserCollaborationEquipmentsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentsCountAsync(userId, search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCollaborationEquipmentAsync(string userId, CollaborationEquipments collaborationEquipment)
    {
        var checkCollaborationEquipmentResult = await _collaborationEquipmentsService.IsCollaborationEquipmentDeletedOrInactiveAsync(collaborationEquipment.Id);
        if (checkCollaborationEquipmentResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertOrUpdateResult = await _userCollaborationEquipmentsRepository.InsertOrUpdateUserCollaborationEquipmentAsync(userId, collaborationEquipment);

        if (insertOrUpdateResult == null || insertOrUpdateResult.OperationType == DatabaseOperationType.None)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        if (insertOrUpdateResult.OperationType == DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = true,
                OperationType = DatabaseOperationType.Updated,
                IsChangePower = false,
                Message = MessageConstants.UPDATED_SUCCESSFULLY
            };
        }

        await _collaborationEquipmentsGalleryService.InsertCollaborationEquipmentGalleryAsync(userId, collaborationEquipment.Id);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Inserted,
            IsChangePower = false,
            Message = MessageConstants.INSERTED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserCollaborationEquipmentsBatchAsync(string userId, List<CollaborationEquipments> collaborationEquipments)
    {
        var insertOrUpdateResult = await _userCollaborationEquipmentsRepository.InsertOrUpdateUserCollaborationEquipmentsBatchAsync(userId, collaborationEquipments);

        if (insertOrUpdateResult?.Data == null || !insertOrUpdateResult.IsSuccess)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        var newlyInsertedCards = insertOrUpdateResult.Data.InsertedItems;
        bool hasNewInserts = newlyInsertedCards != null && newlyInsertedCards.Count > 0;

        if (hasNewInserts)
        {
            await _collaborationEquipmentsGalleryService.InsertBatchCollaborationEquipmentsGalleryAsync(userId, newlyInsertedCards);
        }

        return insertOrUpdateResult.OperationType switch
        {
            DatabaseOperationType.Mixed => InsertOrUpdateResult<bool>.Mixed(true),
            DatabaseOperationType.Inserted => InsertOrUpdateResult<bool>.Inserted(true),
            DatabaseOperationType.Updated => InsertOrUpdateResult<bool>.Updated(true),
            _ => new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = insertOrUpdateResult.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            }
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserCollaborationEquipmentLevelAsync(string userId, CollaborationEquipments collaborationEquipment, List<RecipeItemDetailDTO> totalDeductions)
    {
        if (collaborationEquipment == null)
        {
            return InsertOrUpdateResult<bool>.Failure("CollaborationEquipment is required.");
        }

        var checkResult = await _collaborationEquipmentsService.IsCollaborationEquipmentDeletedOrInactiveAsync(collaborationEquipment.Id);
        if (checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userCollaborationEquipmentsRepository.UpdateUserCollaborationEquipmentLevelAsync(
            userId,
            collaborationEquipment,
            totalDeductions);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserCollaborationEquipmentStarAsync(string userId, CollaborationEquipments collaborationEquipment)
    {
        var checkCollaborationEquipmentResult = await _collaborationEquipmentsService.IsCollaborationEquipmentDeletedOrInactiveAsync(collaborationEquipment.Id);
        if (checkCollaborationEquipmentResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _userCollaborationEquipmentsRepository.UpdateUserCollaborationEquipmentStarAsync(userId, collaborationEquipment);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                IsChangePower = false,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        await _collaborationEquipmentsGalleryService.UpdateTempStarCollaborationEquipmentGalleryAsync(userId, collaborationEquipment.Id, collaborationEquipment.Star);

        return new InsertOrUpdateResult<bool>
        {
            Data = true,
            OperationType = DatabaseOperationType.Updated,
            IsChangePower = false,
            Message = MessageConstants.UPDATED_SUCCESSFULLY
        };
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateUserCollaborationEquipmentQuantityAsync(string userId, CollaborationEquipments collaborationEquipment, List<RecipeItemDetailDTO> totalDeductions)
    {
        var result = await _userCollaborationEquipmentsRepository.UpdateUserCollaborationEquipmentQuantityAsync(userId, collaborationEquipment, totalDeductions);

        return result;
    }

    public async Task<CollaborationEquipments> GetUserCollaborationEquipmentByIdAsync(string userId, string Id)
    {
        var result = await _userCollaborationEquipmentsRepository.GetUserCollaborationEquipmentByIdAsync(userId, Id);

        result.BaseStats = new BaseStats(result);

        result = PowerEvaluatorHelper.EvaluatePower(result);

        return result;
    }

    public async Task<CollaborationEquipments> SumPowerUserCollaborationEquipmentsAsync(string userId)
    {
        return await _userCollaborationEquipmentsRepository.SumPowerUserCollaborationEquipmentsAsync(userId);
    }
}
