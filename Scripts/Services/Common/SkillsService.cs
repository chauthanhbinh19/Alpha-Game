using System.Collections.Generic;

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

    public List<string> GetUniqueSkillsTypes()
    {
        return _skillsRepository.GetUniqueSkillsTypes();
    }

    public List<Skills> GetSkills(string type, int pageSize, int offset)
    {
        List<Skills> list = _skillsRepository.GetSkills(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetSkillsCount(string type)
    {
        return _skillsRepository.GetSkillsCount(type);
    }

    public List<Skills> GetSkillsWithPrice(string type, int pageSize, int offset)
    {
        List<Skills> list = _skillsRepository.GetSkillsWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetSkillsWithPriceCount(string type)
    {
        return _skillsRepository.GetSkillsWithPriceCount(type);
    }

    public Skills GetSkillsById(string Id)
    {
        return _skillsRepository.GetSkillsById(Id);
    }

    public List<string> GetUniqueSkillsId()
    {
        return _skillsRepository.GetUniqueSkillsId();
    }
}
