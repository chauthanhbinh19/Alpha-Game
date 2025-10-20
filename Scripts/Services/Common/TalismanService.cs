using System.Collections.Generic;

public class TalismanService : ITalismanService
{
    private readonly ITalismanRepository _talismanRepository;

    public TalismanService(ITalismanRepository talismanRepository)
    {
        _talismanRepository = talismanRepository;
    }

    public static TalismanService Create()
    {
        return new TalismanService(new TalismanRepository());
    }

    public List<Talismans> GetTalisman(string type, int pageSize, int offset, string rare)
    {
        List<Talismans> list = _talismanRepository.GetTalisman(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTalismanCount(string type, string rare)
    {
        return _talismanRepository.GetTalismanCount(type, rare);
    }

    public List<Talismans> GetTalismanWithPrice(string type, int pageSize, int offset)
    {
        List<Talismans> list = _talismanRepository.GetTalismanWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetTalismanWithPriceCount(string type)
    {
        return _talismanRepository.GetTalismanWithPriceCount(type);
    }

    public Talismans GetTalismanById(string Id)
    {
        return _talismanRepository.GetTalismanById(Id);
    }

    public Talismans SumPowerTalismanPercent()
    {
        return _talismanRepository.SumPowerTalismanPercent();
    }

    public List<string> GetUniqueTalismanTypes()
    {
        return _talismanRepository.GetUniqueTalismanTypes();
    }

    public List<string> GetUniqueTalismanId()
    {
        return _talismanRepository.GetUniqueTalismanId();
    }
}
