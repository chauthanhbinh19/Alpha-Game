using System.Collections.Generic;

public interface IUserWeaponsRepository
{
    List<Weapons> GetUserWeapons(string user_id, int pageSize, int offset, string rare);
    int GetUserWeaponsCount(string user_id, string rare);
    bool InsertUserWeapons(Weapons Weapons);
    bool UpdateWeaponsLevel(Weapons Weapons, int cardLevel);
    bool UpdateWeaponsBreakthrough(Weapons Weapons, int star, double quantity);
    Weapons GetUserWeaponsById(string user_id, string Id);
    Weapons SumPowerUserWeapons();
}