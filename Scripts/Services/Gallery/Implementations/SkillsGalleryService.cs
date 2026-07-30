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

    public async Task<bool> InsertSkillGalleryAsync(string userId, string Id)
    {
        var insertResult = await _skillsGalleryRepository.InsertSkillGalleryAsync(userId, Id, await _skillsService.GetSkillByIdAsync(Id));

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateStatusSkillGalleryAsync(string userId, string skillId)
    {
        var updateResult = await _skillsGalleryRepository.UpdateStatusSkillGalleryAsync(userId, skillId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated || !updateResult.Data)
        {
            return false;
        }

        PowerManager oldPowerManager = await _powerManagerService.GetUserStatsAsync(userId);
        Skills skillGallery = await GetSkillCollectionByIdAsync(userId, skillId) ?? new Skills();
        PowerManager newPowerManager = oldPowerManager + (PowerManager)skillGallery;

        await _powerManagerService.UpdateUserStatsAsync(userId, newPowerManager);

        return true;
    }

    public async Task<bool> UpdateBatchStatusSkillsGalleryAsync(string userId)
    {
        Skills oldSkill = await SumPowerSkillsGalleryAsync(userId);

        var updateResult = await _skillsGalleryRepository.UpdateBatchStatusSkillsGalleryAsync(userId);

        if (updateResult == null ||
        updateResult.OperationType != DatabaseOperationType.Updated ||
        !updateResult.Data)
        {
            return false;
        }

        Skills newSkill = await SumPowerSkillsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newSkill - (PowerManager)oldSkill;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<Skills> SumPowerSkillsGalleryAsync(string userId)
    {
        return await _skillsGalleryRepository.SumPowerSkillsGalleryAsync(userId);
    }

    public async Task<bool> UpdateStarSkillGalleryAsync(string userId, string Id, double star)
    {
        var updateResult = await _skillsGalleryRepository.UpdateStarSkillGalleryAsync(userId, Id, star);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateCurrentStarSkillGalleryAsync(string userId, string skillId)
    {
        Skills oldSkill = await GetSkillCollectionByIdAsync(userId, skillId) ?? new Skills();

        var updateResult = await _skillsGalleryRepository.UpdateCurrentStarSkillGalleryAsync(userId, skillId);

        if (updateResult == null || updateResult.OperationType != DatabaseOperationType.Updated)
        {
            return false;
        }

        Skills newSkill = await GetSkillCollectionByIdAsync(userId, skillId) ?? new Skills();
        PowerManager deltaPower = (PowerManager)newSkill - (PowerManager)oldSkill;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> UpdateBatchCurrentStarSkillsGalleryAsync(string userId)
    {
        Skills oldSkill = await SumPowerSkillsGalleryAsync(userId);

        var updateResult = await _skillsGalleryRepository.UpdateBatchCurrentStarSkillsGalleryAsync(userId);

        if (updateResult == null ||
            updateResult.OperationType != DatabaseOperationType.Updated ||
            updateResult.Data == null ||
            !updateResult.Data.Any())
        {
            return false;
        }

        Skills newSkill = await SumPowerSkillsGalleryAsync(userId);
        PowerManager deltaPower = (PowerManager)newSkill - (PowerManager)oldSkill;

        if (deltaPower.Power == 0)
        {
            return false;
        }

        PowerManager currentPower = await _powerManagerService.GetUserStatsAsync(userId);
        PowerManager updatedPower = currentPower + deltaPower;

        await _powerManagerService.UpdateUserStatsAsync(userId, updatedPower);

        return true;
    }

    public async Task<bool> InsertBatchSkillsGalleryAsync(string userId, List<Skills> skills)
    {
        var insertResult = await _skillsGalleryRepository.InsertBatchSkillsGalleryAsync(userId, skills);

        if (insertResult == null || insertResult.OperationType != DatabaseOperationType.Inserted)
        {
            return false;
        }

        return true;
    }

    public async Task<Skills> GetSkillCollectionByIdAsync(string userId, string skillId)
    {
        var result = await _skillsGalleryRepository.GetSkillCollectionByIdAsync(userId, skillId);
        result = StarEvaluatorHelper.GetStarGalleryPower(result);
        return result;
    }

    public async Task UpdateSkillGalleryPowerAsync(string userId, string Id)
    {
        ISkillsRepository _repository = new SkillsRepository();
        SkillsService _service = new SkillsService(_repository);
        await _skillsGalleryRepository.UpdateSkillGalleryPowerAsync(userId, Id, await _service.GetSkillByIdAsync(Id));
    }
}
