using System.Collections.Generic;

public class WeaponsService : IWeaponsService
{
    private readonly IWeaponsRepository _WeaponsRepository;

    public WeaponsService(IWeaponsRepository titleRepository)
    {
        _WeaponsRepository = titleRepository;
    }

    public static WeaponsService Create()
    {
        return new WeaponsService(new WeaponsRepository());
    }

    public List<Weapons> GetWeapons(int pageSize, int offset, string rare)
    {
        List<Weapons> list = _WeaponsRepository.GetWeapons(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetWeaponsCount(string rare)
    {
        return _WeaponsRepository.GetWeaponsCount(rare);
    }

    public List<Weapons> GetWeaponsWithPrice(int pageSize, int offset)
    {
        List<Weapons> list = _WeaponsRepository.GetWeaponsWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetWeaponsWithPriceCount()
    {
        return _WeaponsRepository.GetWeaponsWithPriceCount();
    }

    public Weapons GetWeaponsById(string Id)
    {
        return _WeaponsRepository.GetWeaponsById(Id);
    }

    public Weapons SumPowerWeaponsPercent()
    {
        return _WeaponsRepository.SumPowerWeaponsPercent();
    }

    public List<string> GetUniqueWeaponId()
    {
        return _WeaponsRepository.GetUniqueWeaponId();
    }
}
