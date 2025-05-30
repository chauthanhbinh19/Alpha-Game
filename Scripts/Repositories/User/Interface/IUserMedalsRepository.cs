using System.Collections.Generic;

public interface IUserMedalsRepository
{
    List<Medals> GetUserMedals(string user_id, int pageSize, int offset);
    int GetUserMedalsCount(string user_id);
    bool InsertUserMedals(Medals medals);
    bool UpdateMedalsLevel(Medals medals, int cardLevel);
    bool UpdateMedalsBreakthrough(Medals medals, int star, int quantity);
    Medals GetUserMedalsById(string user_id, string Id);
    Medals SumPowerUserMedals();

}