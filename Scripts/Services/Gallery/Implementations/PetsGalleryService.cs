using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PetsGalleryService : IPetsGalleryService
{
    private readonly IPetsGalleryRepository _petsGalleryRepository;
    private readonly IPetsService _petsService;
    private readonly IPowerManagerService _powerManagerService;

    public PetsGalleryService(
        IPetsGalleryRepository petsGalleryRepository,
        IPetsService petsService,
        IPowerManagerService powerManagerService)
    {
        _petsGalleryRepository = petsGalleryRepository;
        _petsService = petsService;
        _powerManagerService = powerManagerService;
    }

    public static IPetsGalleryService Create() => ServiceContainer.GetService<IPetsGalleryService>();

    public async Task<List<Pets>> GetPetsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Pets> list = await _petsGalleryRepository.GetPetsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetPetsCountAsync(string search, string type, string rare)
    {
        return await _petsGalleryRepository.GetPetsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertPetGalleryAsync(string userId, string Id)
    {
        var checkResult = await _petsService.IsPetDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _petsGalleryRepository.InsertPetGalleryAsync(userId, Id, await _petsService.GetPetByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusPetGalleryAsync(string userId, string petId)
    {
        var checkResult = await _petsService.IsPetDeletedOrInactiveAsync(petId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _petsGalleryRepository.UpdateStatusPetGalleryAsync(userId, petId);

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
        Pets petGallery = await GetPetCollectionByIdAsync(userId, petId) ?? new Pets();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)petGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusPetsGalleryAsync(string userId)
    {
        Pets oldPet = await SumPowerPetsGalleryAsync(userId);

        var updateResult = await _petsGalleryRepository.UpdateBatchStatusPetsGalleryAsync(userId);

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

        Pets newPet = await SumPowerPetsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newPet - (PowerManager)oldPet;

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

    public async Task<Pets> SumPowerPetsGalleryAsync(string userId)
    {
        return await _petsGalleryRepository.SumPowerPetsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarPetGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _petsService.IsPetDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _petsGalleryRepository.UpdateTempStarPetGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarPetGalleryAsync(string userId, string petId)
    {
        var checkResult = await _petsService.IsPetDeletedOrInactiveAsync(petId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Pets oldPet = await GetPetCollectionByIdAsync(userId, petId) ?? new Pets();

        var updateResult = await _petsGalleryRepository.UpdateCurrentStarPetGalleryAsync(userId, petId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Pets newPet = await GetPetCollectionByIdAsync(userId, petId) ?? new Pets();
        PowerManager deltaPower = (PowerManager)newPet - (PowerManager)oldPet;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarPetsGalleryAsync(string userId)
    {
        Pets oldPet = await SumPowerPetsGalleryAsync(userId);

        var updateResult = await _petsGalleryRepository.UpdateBatchCurrentStarPetsGalleryAsync(userId);

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

        Pets newPet = await SumPowerPetsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newPet - (PowerManager)oldPet;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchPetsGalleryAsync(string userId, List<Pets> pets)
    {
        var insertResult = await _petsGalleryRepository.InsertBatchPetsGalleryAsync(userId, pets);

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

    public async Task<Pets> GetPetCollectionByIdAsync(string userId, string petId)
    {
        var result = await _petsGalleryRepository.GetPetCollectionByIdAsync(userId, petId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdatePetGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _petsService.IsPetDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        IPetsRepository _repository = new PetsRepository();
        PetsService _service = new PetsService(_repository);
        await _petsGalleryRepository.UpdatePetGalleryPowerAsync(userId, Id, await _service.GetPetByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
