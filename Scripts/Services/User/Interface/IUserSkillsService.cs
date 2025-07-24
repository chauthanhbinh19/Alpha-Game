using System.Collections.Generic;

public interface IUserSkillsService
{
    Skills GetNewLevelPower(Skills c, double coefficient);
    Skills GetNewBreakthroughPower(Skills c, double coefficient);
    List<Skills> GetUserSkills(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserSkillsCount(string user_id, string type, string rare);
    bool InsertUserSkills(Skills skills);
    bool UpdateSkillsLevel(Skills skills, int cardLevel);
    bool UpdateSkillsBreakthrough(Skills skills, int star, int quantity);
    Skills GetUserSkillsById(string user_id, string Id);
}