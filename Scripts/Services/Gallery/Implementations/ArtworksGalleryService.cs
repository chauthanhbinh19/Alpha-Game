using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ArtworksGalleryService : IArtworksGalleryService
{
    private readonly IArtworksGalleryRepository _artworksGalleryRepository;
    private readonly IArtworksService _artworksService;
    private readonly IPowerManagerService _powerManagerService;

    public ArtworksGalleryService(
        IArtworksGalleryRepository artworksGalleryRepository,
        IArtworksService artworksService,
        IPowerManagerService powerManagerService)
    {
        _artworksGalleryRepository = artworksGalleryRepository;
        _artworksService = artworksService;
        _powerManagerService = powerManagerService;
    }

    public static IArtworksGalleryService Create() => ServiceContainer.GetService<IArtworksGalleryService>();

    public async Task<List<Artworks>> GetArtworksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Artworks> list = await _artworksGalleryRepository.GetArtworksCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtworksCountAsync(string search, string type, string rare)
    {
        return await _artworksGalleryRepository.GetArtworksCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertArtworkGalleryAsync(string userId, string Id)
    {
        var checkResult = await _artworksService.IsArtworkDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _artworksGalleryRepository.InsertArtworkGalleryAsync(userId, Id, await _artworksService.GetArtworkByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusArtworkGalleryAsync(string userId, string artworkId)
    {
        var checkResult = await _artworksService.IsArtworkDeletedOrInactiveAsync(artworkId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _artworksGalleryRepository.UpdateStatusArtworkGalleryAsync(userId, artworkId);

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
        Artworks artworkGallery = await GetArtworkCollectionByIdAsync(userId, artworkId) ?? new Artworks();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)artworkGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusArtworksGalleryAsync(string userId)
    {
        Artworks oldArtwork = await SumPowerArtworksGalleryAsync(userId);

        var updateResult = await _artworksGalleryRepository.UpdateBatchStatusArtworksGalleryAsync(userId);

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

        Artworks newArtwork = await SumPowerArtworksGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newArtwork - (PowerManager)oldArtwork;

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

    public async Task<Artworks> SumPowerArtworksGalleryAsync(string userId)
    {
        return await _artworksGalleryRepository.SumPowerArtworksGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarArtworkGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _artworksService.IsArtworkDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _artworksGalleryRepository.UpdateTempStarArtworkGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarArtworkGalleryAsync(string userId, string artworkId)
    {
        var checkResult = await _artworksService.IsArtworkDeletedOrInactiveAsync(artworkId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Artworks oldArtwork = await GetArtworkCollectionByIdAsync(userId, artworkId) ?? new Artworks();

        var updateResult = await _artworksGalleryRepository.UpdateCurrentStarArtworkGalleryAsync(userId, artworkId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Artworks newArtwork = await GetArtworkCollectionByIdAsync(userId, artworkId) ?? new Artworks();
        PowerManager deltaPower = (PowerManager)newArtwork - (PowerManager)oldArtwork;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarArtworksGalleryAsync(string userId)
    {
        Artworks oldArtwork = await SumPowerArtworksGalleryAsync(userId);

        var updateResult = await _artworksGalleryRepository.UpdateBatchCurrentStarArtworksGalleryAsync(userId);

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

        Artworks newArtwork = await SumPowerArtworksGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newArtwork - (PowerManager)oldArtwork;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchArtworksGalleryAsync(string userId, List<Artworks> artworks)
    {
        var insertResult = await _artworksGalleryRepository.InsertBatchArtworksGalleryAsync(userId, artworks);

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

    public async Task<Artworks> GetArtworkCollectionByIdAsync(string userId, string artworkId)
    {
        var result = await _artworksGalleryRepository.GetArtworkCollectionByIdAsync(userId, artworkId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateArtworkGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _artworksService.IsArtworkDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IArtworksRepository _repository = new ArtworksRepository();
        ArtworksService _service = new ArtworksService(_repository);
        await _artworksGalleryRepository.UpdateArtworkGalleryPowerAsync(userId, Id, await _service.GetArtworkByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
