using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ArtifactsGalleryService : IArtifactsGalleryService
{
    private readonly IArtifactsGalleryRepository _artifactsGalleryRepository;
    private readonly IArtifactsService _artifactsService;
    private readonly IPowerManagerService _powerManagerService;

    public ArtifactsGalleryService(
        IArtifactsGalleryRepository artifactsGalleryRepository,
        IArtifactsService artifactsService,
        IPowerManagerService powerManagerService)
    {
        _artifactsGalleryRepository = artifactsGalleryRepository;
        _artifactsService = artifactsService;
        _powerManagerService = powerManagerService;
    }

    public static IArtifactsGalleryService Create() => ServiceContainer.GetService<IArtifactsGalleryService>();

    public async Task<List<Artifacts>> GetArtifactsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Artifacts> list = await _artifactsGalleryRepository.GetArtifactsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetArtifactsCountAsync(string search, string rare)
    {
        return await _artifactsGalleryRepository.GetArtifactsCountAsync(search, rare);
    }

    public async Task<bool> InsertArtifactGalleryAsync(string userId, string Id)
    {
        var insertResult = await _artifactsGalleryRepository.InsertArtifactGalleryAsync(userId, Id, await _artifactsService.GetArtifactByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusArtifactGalleryAsync(string userId, string artifactId)
    {
        var updateResult = await _artifactsGalleryRepository.UpdateStatusArtifactGalleryAsync(userId, artifactId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Artifacts artifactGallery = await GetArtifactCollectionByIdAsync(userId, artifactId) ?? new Artifacts();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)artifactGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusArtifactsGalleryAsync(string userId)
    {
        Artifacts oldArtifact = await SumPowerArtifactsGalleryAsync(userId);

        var updateResult = await _artifactsGalleryRepository.UpdateBatchStatusArtifactsGalleryAsync(userId);

        if (updateResult == null || 
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Artifacts newArtifact = await SumPowerArtifactsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newArtifact - (PowerManager)oldArtifact;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Artifacts> SumPowerArtifactsGalleryAsync(string userId)
    {
        return await _artifactsGalleryRepository.SumPowerArtifactsGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarArtifactGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _artifactsGalleryRepository.UpdateStarArtifactGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarArtifactGalleryAsync(string userId, string artifactId)
    {
        Artifacts oldArtifact = await GetArtifactCollectionByIdAsync(userId, artifactId) ?? new Artifacts();

        var updateResult = await _artifactsGalleryRepository.UpdateCurrentStarArtifactGalleryAsync(userId, artifactId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Artifacts newArtifact = await GetArtifactCollectionByIdAsync(userId, artifactId) ?? new Artifacts();
        PowerManager deltaPower = (PowerManager)newArtifact - (PowerManager)oldArtifact;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarArtifactsGalleryAsync(string userId)
    {
        Artifacts oldArtifact = await SumPowerArtifactsGalleryAsync(userId);

        var updateResult = await _artifactsGalleryRepository.UpdateBatchCurrentStarArtifactsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Artifacts newArtifact = await SumPowerArtifactsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newArtifact - (PowerManager)oldArtifact;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchArtifactsGalleryAsync(string userId, List<Artifacts> artifacts)
    {
        var insertResult = await _artifactsGalleryRepository.InsertBatchArtifactsGalleryAsync(userId, artifacts);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Artifacts> GetArtifactCollectionByIdAsync(string userId, string artifactId)
    {
        var result = await _artifactsGalleryRepository.GetArtifactCollectionByIdAsync(userId, artifactId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateArtifactGalleryPowerAsync(string userId, string Id)
    {
        IArtifactsRepository _repository = new ArtifactsRepository();
        ArtifactsService _service = new ArtifactsService(_repository);
        await _artifactsGalleryRepository.UpdateArtifactGalleryPowerAsync(userId, Id, await _service.GetArtifactByIdAsync(Id));
    }
}
