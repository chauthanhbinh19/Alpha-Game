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

    public async Task<bool> InsertAvatarGalleryAsync(string userId, string Id)
    {
        var insertResult = await _avatarsGalleryRepository.InsertAvatarGalleryAsync(userId, Id, await _avatarsService.GetAvatarByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusAvatarGalleryAsync(string userId, string avatarId)
    {
        var updateResult = await _avatarsGalleryRepository.UpdateStatusAvatarGalleryAsync(userId, avatarId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Avatars avatarGallery = await GetAvatarCollectionByIdAsync(userId, avatarId) ?? new Avatars();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)avatarGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusAvatarsGalleryAsync(string userId)
    {
        Avatars oldAvatar = await SumPowerAvatarsGalleryAsync(userId);

        var updateResult = await _avatarsGalleryRepository.UpdateBatchStatusAvatarsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Avatars newAvatar = await SumPowerAvatarsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newAvatar - (PowerManager)oldAvatar;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Avatars> SumPowerAvatarsGalleryAsync(string userId)
    {
        return await _avatarsGalleryRepository.SumPowerAvatarsGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarAvatarGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _avatarsGalleryRepository.UpdateStarAvatarGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarAvatarGalleryAsync(string userId, string avatarId)
    {
        Avatars oldAvatar = await GetAvatarCollectionByIdAsync(userId, avatarId) ?? new Avatars();

        var updateResult = await _avatarsGalleryRepository.UpdateCurrentStarAvatarGalleryAsync(userId, avatarId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Avatars newAvatar = await GetAvatarCollectionByIdAsync(userId, avatarId) ?? new Avatars();
        PowerManager deltaPower = (PowerManager)newAvatar - (PowerManager)oldAvatar;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarAvatarsGalleryAsync(string userId)
    {
        Avatars oldAvatar = await SumPowerAvatarsGalleryAsync(userId);

        var updateResult = await _avatarsGalleryRepository.UpdateBatchCurrentStarAvatarsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Avatars newAvatar = await SumPowerAvatarsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newAvatar - (PowerManager)oldAvatar;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchAvatarsGalleryAsync(string userId, List<Avatars> avatars)
    {
        var insertResult = await _avatarsGalleryRepository.InsertBatchAvatarsGalleryAsync(userId, avatars);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Avatars> GetAvatarCollectionByIdAsync(string userId, string avatarId)
    {
        var result = await _avatarsGalleryRepository.GetAvatarCollectionByIdAsync(userId, avatarId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateAvatarGalleryPowerAsync(string userId, string Id)
    {
        IAvatarsRepository _repository = new AvatarsRepository();
        AvatarsService _service = new AvatarsService(_repository);
        await _avatarsGalleryRepository.UpdateAvatarGalleryPowerAsync(userId, Id, await _service.GetAvatarByIdAsync(Id));
    }
}