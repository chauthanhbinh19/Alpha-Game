using System.Collections.Generic;
using System.Threading.Tasks;

public class SkillsGalleryService : ISkillsGalleryService
{
    private readonly ISkillsGalleryRepository _skillsGalleryRepository;

    public SkillsGalleryService(ISkillsGalleryRepository skillsGalleryRepository)
    {
        _skillsGalleryRepository = skillsGalleryRepository;
    }

    public static SkillsGalleryService Create()
    {
        return new SkillsGalleryService(new SkillsGalleryRepository());
    }

    public async Task<List<Skills>> GetSkillsCollectionAsync(string type, int pageSize, int offset, string rare)
    {
        List<Skills> list = await _skillsGalleryRepository.GetSkillsCollectionAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSkillsCountAsync(string type, string rare)
    {
        return await _skillsGalleryRepository.GetSkillsCountAsync(type, rare);
    }

    public async Task InsertSkillGalleryAsync(string Id)
    {
        ISkillsRepository _repository = new SkillsRepository();
        SkillsService _service = new SkillsService(_repository);
        await _skillsGalleryRepository.InsertSkillGalleryAsync(Id, await _service.GetSkillByIdAsync(Id));
    }

    public async Task UpdateStatusSkillGalleryAsync(string Id)
    {
        await _skillsGalleryRepository.UpdateStatusSkillGalleryAsync(Id);
    }

    public async Task<Skills> SumPowerSkillsGalleryAsync()
    {
        return await _skillsGalleryRepository.SumPowerSkillsGalleryAsync();
    }

    public async Task UpdateStarSkillGalleryAsync(string Id, double star)
    {
        await _skillsGalleryRepository.UpdateStarSkillGalleryAsync(Id, star);
    }

    public async Task UpdateSkillGalleryPowerAsync(string Id)
    {
        ISkillsRepository _repository = new SkillsRepository();
        SkillsService _service = new SkillsService(_repository);
        await _skillsGalleryRepository.UpdateSkillGalleryPowerAsync(Id, await _service.GetSkillByIdAsync(Id));
    }
}
