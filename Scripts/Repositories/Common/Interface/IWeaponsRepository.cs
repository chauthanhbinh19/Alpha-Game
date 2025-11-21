using System.Collections.Generic;

public interface IWeaponsRepository
{
    List<string> GetUniqueWeaponId();
    List<Weapons> GetWeapons(int pageSize, int offset, string rare);
    int GetWeaponsCount(string rare);
    List<Weapons> GetWeaponsWithPrice(int pageSize, int offset);
    int GetWeaponsWithPriceCount();
    Weapons GetWeaponsById(string Id);
    Weapons SumPowerWeaponsPercent();
}
