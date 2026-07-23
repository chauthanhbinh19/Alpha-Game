using System.Collections.Generic;
using System.Threading.Tasks;

public class SkillsGalleryService : ISkillsGalleryService
{
    private static SkillsGalleryService _instance;
    private readonly ISkillsGalleryRepository _skillsGalleryRepository;

    public SkillsGalleryService(ISkillsGalleryRepository skillsGalleryRepository)
    {
        _skillsGalleryRepository = skillsGalleryRepository;
    }

    public static SkillsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new SkillsGalleryService(new SkillsGalleryRepository());
        }
        return _instance;
    }

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

    public async Task InsertSkillGalleryAsync(string userId, string Id)
    {
        ISkillsRepository _repository = new SkillsRepository();
        SkillsService _service = new SkillsService(_repository);
        await _skillsGalleryRepository.InsertSkillGalleryAsync(userId, Id, await _service.GetSkillByIdAsync(Id));
    }

    public async Task UpdateStatusSkillGalleryAsync(string userId, string Id)
    {
        await _skillsGalleryRepository.UpdateStatusSkillGalleryAsync(userId, Id);
    }

    public async Task<Skills> SumPowerSkillsGalleryAsync(string userId)
    {
        return await _skillsGalleryRepository.SumPowerSkillsGalleryAsync(userId);
    }

    public async Task UpdateStarSkillGalleryAsync(string userId, string Id, double star)
    {
        await _skillsGalleryRepository.UpdateStarSkillGalleryAsync(userId, Id, star);
    }

    public async Task UpdateSkillGalleryPowerAsync(string userId, string Id)
    {
        ISkillsRepository _repository = new SkillsRepository();
        SkillsService _service = new SkillsService(_repository);
        await _skillsGalleryRepository.UpdateSkillGalleryPowerAsync(userId, Id, await _service.GetSkillByIdAsync(Id));
    }
}
