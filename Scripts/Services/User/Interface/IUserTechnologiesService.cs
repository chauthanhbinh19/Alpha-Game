using System.Collections.Generic;

public interface IUserTechnologiesService
{
    Technologies GetNewLevelPower(Technologies c, double coefficient);
    Technologies GetNewBreakthroughPower(Technologies c, double coefficient);
    List<Technologies> GetUserTechnologies(string user_id, int pageSize, int offset, string rare);
    int GetUserTechnologiesCount(string user_id, string rare);
    bool InsertUserTechnologies(Technologies Technologies);
    bool UpdateTechnologiesLevel(Technologies Technologies, int cardLevel);
    bool UpdateTechnologiesBreakthrough(Technologies Technologies, int star, double quantity);
    Technologies GetUserTechnologiesById(string user_id, string Id);
    Technologies SumPowerUserTechnologies();
}