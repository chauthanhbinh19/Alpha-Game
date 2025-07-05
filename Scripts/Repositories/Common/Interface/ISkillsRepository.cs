using System.Collections.Generic;
public interface ISkillsRepository
{
    List<string> GetUniqueSkillsTypes();
    List<string> GetUniqueSkillsId();
    List<Skills> GetSkills(string type, int pageSize, int offset);
    int GetSkillsCount(string type);
    List<Skills> GetSkillsWithPrice(string type, int pageSize, int offset);
    int GetSkillsWithPriceCount(string type);
    Skills GetSkillsById(string Id);
}
