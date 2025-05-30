using System.Collections.Generic;

public interface IUserSkillsRepository
{
    List<Skills> GetUserSkills(string user_id, string type, int pageSize, int offset);
    int GetUserSkillsCount(string user_id, string type);
    bool InsertUserSkills(Skills skills);
    bool UpdateSkillsLevel(Skills skills, int cardLevel);
    bool UpdateSkillsBreakthrough(Skills skills, int star, int quantity);
    Skills GetUserSkillsById(string user_id, string id);
}