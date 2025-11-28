using System.Collections.Generic;

public interface IUserMedalsRepository
{
    List<Medals> GetUserMedals(string user_id, int pageSize, int offset, string rare);
    int GetUserMedalsCount(string user_id, string rare);
    bool InsertUserMedals(Medals medals, string userId);
    bool UpdateMedalsLevel(Medals medals, int cardLevel);
    bool UpdateMedalsBreakthrough(Medals medals, int star, double quantity);
    Medals GetUserMedalsById(string user_id, string Id);
    Medals SumPowerUserMedals();

}