using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SkillsGalleryService : ISkillsGalleryService
{
    private readonly ISkillsGalleryRepository _skillsGalleryRepository;
    private readonly ISkillsService _skillsService;
    private readonly IPowerManagerService _powerManagerService;

    public SkillsGalleryService(
        ISkillsGalleryRepository skillsGalleryRepository,
        ISkillsService skillsService,
        IPowerManagerService powerManagerService)
    {
        _skillsGalleryRepository = skillsGalleryRepository;
        _skillsService = skillsService;
        _powerManagerService = powerManagerService;
    }

    public static ISkillsGalleryService Create() => ServiceContainer.GetService<ISkillsGalleryService>();

    public async Task<List<Skills>> GetSkillsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Skills> list = await _skillsGalleryRepository.GetSkillsCollectionAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSkillsCountAsync(string search, string type, string rare)
    {
        return await _skillsGalleryRepository.GetSkillsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertSkillGalleryAsync(string userId, string Id)
    {
        var checkResult = await _skillsService.IsSkillDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var insertResult = await _skillsGalleryRepository.InsertSkillGalleryAsync(userId, Id, await _skillsService.GetSkillByIdAsync(Id));

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

    public async Task<InsertOrUpdateResult<bool>> UpdateStatusSkillGalleryAsync(string userId, string skillId)
    {
        var checkResult = await _skillsService.IsSkillDeletedOrInactiveAsync(skillId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _skillsGalleryRepository.UpdateStatusSkillGalleryAsync(userId, skillId);

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
        Skills skillGallery = await GetSkillCollectionByIdAsync(userId, skillId) ?? new Skills();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)skillGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return InsertOrUpdateResult<bool>.Updated(true);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusSkillsGalleryAsync(string userId)
    {
        Skills oldSkill = await SumPowerSkillsGalleryAsync(userId);

        var updateResult = await _skillsGalleryRepository.UpdateBatchStatusSkillsGalleryAsync(userId);

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

        Skills newSkill = await SumPowerSkillsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newSkill - (PowerManager)oldSkill;

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

    public async Task<Skills> SumPowerSkillsGalleryAsync(string userId)
    {
        return await _skillsGalleryRepository.SumPowerSkillsGalleryAsync(userId);
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateTempStarSkillGalleryAsync(string userId, string Id, double star)
    {
        var checkResult = await _skillsService.IsSkillDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        var updateResult = await _skillsGalleryRepository.UpdateTempStarSkillGalleryAsync(userId, Id, star);

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

    public async Task<InsertOrUpdateResult<bool>> UpdateCurrentStarSkillGalleryAsync(string userId, string skillId)
    {
        var checkResult = await _skillsService.IsSkillDeletedOrInactiveAsync(skillId);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        Skills oldSkill = await GetSkillCollectionByIdAsync(userId, skillId) ?? new Skills();

        var updateResult = await _skillsGalleryRepository.UpdateCurrentStarSkillGalleryAsync(userId, skillId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = updateResult?.Message ?? MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        Skills newSkill = await GetSkillCollectionByIdAsync(userId, skillId) ?? new Skills();
        PowerManager deltaPower = (PowerManager)newSkill - (PowerManager)oldSkill;

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

    public async Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarSkillsGalleryAsync(string userId)
    {
        Skills oldSkill = await SumPowerSkillsGalleryAsync(userId);

        var updateResult = await _skillsGalleryRepository.UpdateBatchCurrentStarSkillsGalleryAsync(userId);

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

        Skills newSkill = await SumPowerSkillsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newSkill - (PowerManager)oldSkill;

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

    public async Task<InsertOrUpdateResult<bool>> InsertBatchSkillsGalleryAsync(string userId, List<Skills> skills)
    {
        var insertResult = await _skillsGalleryRepository.InsertBatchSkillsGalleryAsync(userId, skills);

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

    public async Task<Skills> GetSkillCollectionByIdAsync(string userId, string skillId)
    {
        var result = await _skillsGalleryRepository.GetSkillCollectionByIdAsync(userId, skillId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateSkillGalleryPowerAsync(string userId, string Id)
    {
        var checkResult = await _skillsService.IsSkillDeletedOrInactiveAsync(Id);
        if(checkResult)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        ISkillsRepository _repository = new SkillsRepository();
        SkillsService _service = new SkillsService(_repository);
        await _skillsGalleryRepository.UpdateSkillGalleryPowerAsync(userId, Id, await _service.GetSkillByIdAsync(Id));
        return InsertOrUpdateResult<bool>.Updated(true);
    }
}
