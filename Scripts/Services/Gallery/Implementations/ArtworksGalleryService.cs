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

    public async Task<bool> InsertArtworkGalleryAsync(string userId, string Id)
    {
        var insertResult = await _artworksGalleryRepository.InsertArtworkGalleryAsync(userId, Id, await _artworksService.GetArtworkByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusArtworkGalleryAsync(string userId, string artworkId)
    {
        var updateResult = await _artworksGalleryRepository.UpdateStatusArtworkGalleryAsync(userId, artworkId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Artworks artworkGallery = await GetArtworkCollectionByIdAsync(userId, artworkId) ?? new Artworks();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)artworkGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusArtworksGalleryAsync(string userId)
    {
        Artworks oldArtwork = await SumPowerArtworksGalleryAsync(userId);

        var updateResult = await _artworksGalleryRepository.UpdateBatchStatusArtworksGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Artworks newArtwork = await SumPowerArtworksGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newArtwork - (PowerManager)oldArtwork;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Artworks> SumPowerArtworksGalleryAsync(string userId)
    {
        return await _artworksGalleryRepository.SumPowerArtworksGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarArtworkGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _artworksGalleryRepository.UpdateTempStarArtworkGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarArtworkGalleryAsync(string userId, string artworkId)
    {
        Artworks oldArtwork = await GetArtworkCollectionByIdAsync(userId, artworkId) ?? new Artworks();

        var updateResult = await _artworksGalleryRepository.UpdateCurrentStarArtworkGalleryAsync(userId, artworkId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Artworks newArtwork = await GetArtworkCollectionByIdAsync(userId, artworkId) ?? new Artworks();
        PowerManager deltaPower = (PowerManager)newArtwork - (PowerManager)oldArtwork;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarArtworksGalleryAsync(string userId)
    {
        Artworks oldArtwork = await SumPowerArtworksGalleryAsync(userId);

        var updateResult = await _artworksGalleryRepository.UpdateBatchCurrentStarArtworksGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Artworks newArtwork = await SumPowerArtworksGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newArtwork - (PowerManager)oldArtwork;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchArtworksGalleryAsync(string userId, List<Artworks> artworks)
    {
        var insertResult = await _artworksGalleryRepository.InsertBatchArtworksGalleryAsync(userId, artworks);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Artworks> GetArtworkCollectionByIdAsync(string userId, string artworkId)
    {
        var result = await _artworksGalleryRepository.GetArtworkCollectionByIdAsync(userId, artworkId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateArtworkGalleryPowerAsync(string userId, string Id)
    {
        IArtworksRepository _repository = new ArtworksRepository();
        ArtworksService _service = new ArtworksService(_repository);
        await _artworksGalleryRepository.UpdateArtworkGalleryPowerAsync(userId, Id, await _service.GetArtworkByIdAsync(Id));
    }
}
