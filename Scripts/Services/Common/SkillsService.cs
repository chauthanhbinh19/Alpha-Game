using System.Collections.Generic;
using System.Threading.Tasks;

public class SkillsService : ISkillsService
{
    private readonly ISkillsRepository _skillsRepository;

    public SkillsService(ISkillsRepository skillsRepository)
    {
        _skillsRepository = skillsRepository;
    }

    public static SkillsService Create()
    {
        return new SkillsService(new SkillsRepository());
    }

    public async Task<List<string>> GetUniqueSkillsTypesAsync()
    {
        return await _skillsRepository.GetUniqueSkillsTypesAsync();
    }

    public async Task<List<Skills>> GetSkillsAsync(string type, int pageSize, int offset, string rare)
    {
        List<Skills> list = await _skillsRepository.GetSkillsAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSkillsCountAsync(string type, string rare)
    {
        return await _skillsRepository.GetSkillsCountAsync(type, rare);
    }

    public async Task<List<Skills>> GetSkillsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Skills> list = await _skillsRepository.GetSkillsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSkillsWithPriceCountAsync(string type)
    {
        return await _skillsRepository.GetSkillsWithPriceCountAsync(type);
    }

    public async Task<Skills> GetSkillByIdAsync(string Id)
    {
        return await _skillsRepository.GetSkillByIdAsync(Id);
    }

    public async Task<List<string>> GetUniqueSkillsIdAsync()
    {
        return await _skillsRepository.GetUniqueSkillsIdAsync();
    }
}
