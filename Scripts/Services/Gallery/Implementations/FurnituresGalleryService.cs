using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FurnituresGalleryService : IFurnituresGalleryService
{
    private readonly IFurnituresGalleryRepository _furnituresGalleryRepository;
    private readonly IFurnituresService _furnituresService;
    private readonly IPowerManagerService _powerManagerService;

    public FurnituresGalleryService(
        IFurnituresGalleryRepository furnituresGalleryRepository,
        IFurnituresService furnituresService,
        IPowerManagerService powerManagerService)
    {
        _furnituresGalleryRepository = furnituresGalleryRepository;
        _furnituresService = furnituresService;
        _powerManagerService = powerManagerService;
    }

    public static IFurnituresGalleryService Create() => ServiceContainer.GetService<IFurnituresGalleryService>();

    public async Task<List<Furnitures>> GetFurnituresCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Furnitures> list = await _furnituresGalleryRepository.GetFurnituresCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetFurnituresCountAsync(string search, string type, string rare)
    {
        return await _furnituresGalleryRepository.GetFurnituresCountAsync(search, type, rare);
    }

    public async Task<bool> InsertFurnitureGalleryAsync(string userId, string Id)
    {
        var insertResult = await _furnituresGalleryRepository.InsertFurnitureGalleryAsync(userId, Id, await _furnituresService.GetFurnitureByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusFurnitureGalleryAsync(string userId, string furnitureId)
    {
        var updateResult = await _furnituresGalleryRepository.UpdateStatusFurnitureGalleryAsync(userId, furnitureId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Furnitures furnitureGallery = await GetFurnitureCollectionByIdAsync(userId, furnitureId) ?? new Furnitures();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)furnitureGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusFurnituresGalleryAsync(string userId)
    {
        Furnitures oldFurniture = await SumPowerFurnituresGalleryAsync(userId);

        var updateResult = await _furnituresGalleryRepository.UpdateBatchStatusFurnituresGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Furnitures newFurniture = await SumPowerFurnituresGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newFurniture - (PowerManager)oldFurniture;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Furnitures> SumPowerFurnituresGalleryAsync(string userId)
    {
        return await _furnituresGalleryRepository.SumPowerFurnituresGalleryAsync(userId);
    }

    public async Task<bool> UpdateTempStarFurnitureGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _furnituresGalleryRepository.UpdateTempStarFurnitureGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarFurnitureGalleryAsync(string userId, string furnitureId)
    {
        Furnitures oldFurniture = await GetFurnitureCollectionByIdAsync(userId, furnitureId) ?? new Furnitures();

        var updateResult = await _furnituresGalleryRepository.UpdateCurrentStarFurnitureGalleryAsync(userId, furnitureId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Furnitures newFurniture = await GetFurnitureCollectionByIdAsync(userId, furnitureId) ?? new Furnitures();
        PowerManager deltaPower = (PowerManager)newFurniture - (PowerManager)oldFurniture;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarFurnituresGalleryAsync(string userId)
    {
        Furnitures oldFurniture = await SumPowerFurnituresGalleryAsync(userId);

        var updateResult = await _furnituresGalleryRepository.UpdateBatchCurrentStarFurnituresGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Furnitures newFurniture = await SumPowerFurnituresGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newFurniture - (PowerManager)oldFurniture;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchFurnituresGalleryAsync(string userId, List<Furnitures> furnitures)
    {
        var insertResult = await _furnituresGalleryRepository.InsertBatchFurnituresGalleryAsync(userId, furnitures);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Furnitures> GetFurnitureCollectionByIdAsync(string userId, string furnitureId)
    {
        var result = await _furnituresGalleryRepository.GetFurnitureCollectionByIdAsync(userId, furnitureId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateFurnitureGalleryPowerAsync(string userId, string Id)
    {
        IFurnituresRepository _repository = new FurnituresRepository();
        FurnituresService _service = new FurnituresService(_repository);
        await _furnituresGalleryRepository.UpdateFurnitureGalleryPowerAsync(userId, Id, await _service.GetFurnitureByIdAsync(Id));
    }
}
