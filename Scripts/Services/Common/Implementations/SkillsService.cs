using System.Collections.Generic;
using System.Threading.Tasks;

public class SkillsService : ISkillsService
{
    private static SkillsService _instance;
    private readonly ISkillsRepository _skillsRepository;

    public SkillsService(ISkillsRepository skillsRepository)
    {
        _skillsRepository = skillsRepository;
    }

    public static SkillsService Create()
    {
        if (_instance == null)
        {
            _instance = new SkillsService(new SkillsRepository());
        }
        return _instance;
    }

    public async Task<List<string>> GetUniqueSkillsTypesAsync()
    {
        return await _skillsRepository.GetUniqueSkillsTypesAsync();
    }

    public async Task<List<Skills>> GetSkillsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Skills> list = await _skillsRepository.GetSkillsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSkillsCountAsync(string search, string type, string rare)
    {
        return await _skillsRepository.GetSkillsCountAsync(search, type, rare);
    }

    public async Task<List<Skills>> GetSkillsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Skills> list = await _skillsRepository.GetSkillsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
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
