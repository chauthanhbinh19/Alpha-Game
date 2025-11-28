using System.Collections.Generic;

public interface IUserCoresRepository
{
    List<Cores> GetUserCores(string user_id, int pageSize, int offset, string rare);
    int GetUserCoresCount(string user_id, string rare);
    bool InsertUserCores(Cores Cores, string userId);
    bool UpdateCoresLevel(Cores Cores, int cardLevel);
    bool UpdateCoresBreakthrough(Cores Cores, int star, double quantity);
    Cores GetUserCoresById(string user_id, string Id);
    Cores SumPowerUserCores();
}