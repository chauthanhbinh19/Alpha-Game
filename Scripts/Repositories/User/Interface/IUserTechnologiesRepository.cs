using System.Collections.Generic;

public interface IUserTechnologiesRepository
{
    List<Technologies> GetUserTechnologies(string user_id, int pageSize, int offset, string rare);
    int GetUserTechnologiesCount(string user_id, string rare);
    bool InsertUserTechnologies(Technologies Technologies, string userId);
    bool UpdateTechnologiesLevel(Technologies Technologies, int cardLevel);
    bool UpdateTechnologiesBreakthrough(Technologies Technologies, int star, double quantity);
    Technologies GetUserTechnologiesById(string user_id, string Id);
    Technologies SumPowerUserTechnologies();
}