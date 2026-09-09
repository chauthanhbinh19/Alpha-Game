using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AvatarsGalleryService : IAvatarsGalleryService
{
    private readonly IAvatarsGalleryRepository _avatarsGalleryRepository;
    private readonly IAvatarsService _avatarsService;
    private readonly IPowerManagerService _powerManagerService;

    public AvatarsGalleryService(
        IAvatarsGalleryRepository avatarsGalleryRepository,
        IAvatarsService avatarsService,
        IPowerManagerService powerManagerService)
    {
        _avatarsGalleryRepository = avatarsGalleryRepository;
        _avatarsService = avatarsService;
        _powerManagerService = powerManagerService;
    }

    public static IAvatarsGalleryService Create() => ServiceContainer.GetService<IAvatarsGalleryService>();

    public async Task<List<Avatars>> GetAvatarsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        return await _avatarsGalleryRepository.GetAvatarsCollectionAsync(userId, search, pageSize, offset, rare);
    }

    public async Task<int> GetAvatarsCountAsync(string search, string rare)
    {
        return await _avatarsGalleryRepository.GetAvatarsCountAsync(search, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertAvatarGalleryAsync(string userId, string Id)
    {
        var checkResult = await _avatarsService.IsAvatarDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _avatarsGalleryRepository.InsertAvatarGalleryAsync(userId, Id, await _avatarsService.GetAvatarByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusAvatarGalleryAsync(string userId, string avatarId)
    {
        var checkResult = await _avatarsService.IsAvatarDeletedOrInactiveAsync(avatarId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _avatarsGalleryRepository.UpdateStatusAvatarGalleryAsync(userId, avatarId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Avatars avatarGallery = await GetAvatarCollectionByIdAsync(userId, avatarId) ?? new Avatars();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)avatarGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusAvatarsGalleryAsync(string userId)
    {
        Avatars oldAvatar = await SumPowerAvatarsGalleryAsync(userId);

        var updateResult = await _avatarsGalleryRepository.UpdateBatchStatusAvatarsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Avatars newAvatar = await SumPowerAvatarsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newAvatar - (PowerManager)oldAvatar;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<Avatars> SumPowerAvatarsGalleryAsync(string userId)
    {
        return await _avatarsGalleryRepository.SumPowerAvatarsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarAvatarGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _avatarsService.IsAvatarDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _avatarsGalleryRepository.UpdateTempStarAvatarGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarAvatarGalleryAsync(string userId, string avatarId)
    {
        var checkResult = await _avatarsService.IsAvatarDeletedOrInactiveAsync(avatarId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Avatars oldAvatar = await GetAvatarCollectionByIdAsync(userId, avatarId) ?? new Avatars();

        var updateResult = await _avatarsGalleryRepository.UpdateCurrentStarAvatarGalleryAsync(userId, avatarId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Avatars newAvatar = await GetAvatarCollectionByIdAsync(userId, avatarId) ?? new Avatars();
        PowerManager deltaPower = (PowerManager)newAvatar - (PowerManager)oldAvatar;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarAvatarsGalleryAsync(string userId)
    {
        Avatars oldAvatar = await SumPowerAvatarsGalleryAsync(userId);

        var updateResult = await _avatarsGalleryRepository.UpdateBatchCurrentStarAvatarsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Avatars newAvatar = await SumPowerAvatarsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newAvatar - (PowerManager)oldAvatar;

        if (deltaPower.Power == 0)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.POWER_UNCHANGED_NO_UPDATE_NEEDED
            };
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertBatchAvatarsGalleryAsync(string userId, List<Avatars> avatars)
    {
        var insertResult = await _avatarsGalleryRepository.InsertBatchAvatarsGalleryAsync(userId, avatars);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = insertResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<Avatars> GetAvatarCollectionByIdAsync(string userId, string avatarId)
    {
        var result = await _avatarsGalleryRepository.GetAvatarCollectionByIdAsync(userId, avatarId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateAvatarGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _avatarsService.IsAvatarDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        await _avatarsGalleryRepository.UpdateAvatarGalleryPowerAsync(userId, Id, await _service.GetAvatarByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}