using System.Collections.Generic;

public class SpiritBeastService : ISpiritBeastService
{
    private readonly ISpiritBeastRepository _SpiritBeastRepository;

    public SpiritBeastService(ISpiritBeastRepository titleRepository)
    {
        _SpiritBeastRepository = titleRepository;
    }

    public static SpiritBeastService Create()
    {
        return new SpiritBeastService(new SpiritBeastRepository());
    }

    public List<SpiritBeasts> GetSpiritBeast(int pageSize, int offset, string rare)
    {
        List<SpiritBeasts> list = _SpiritBeastRepository.GetSpiritBeast(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetSpiritBeastCount(string rare)
    {
        return _SpiritBeastRepository.GetSpiritBeastCount(rare);
    }

    public List<SpiritBeasts> GetSpiritBeastWithPrice(int pageSize, int offset)
    {
        List<SpiritBeasts> list = _SpiritBeastRepository.GetSpiritBeastWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetSpiritBeastWithPriceCount()
    {
        return _SpiritBeastRepository.GetSpiritBeastWithPriceCount();
    }

    public SpiritBeasts GetSpiritBeastById(string Id)
    {
        return _SpiritBeastRepository.GetSpiritBeastById(Id);
    }

    public SpiritBeasts SumPowerSpiritBeastPercent()
    {
        return _SpiritBeastRepository.SumPowerSpiritBeastPercent();
    }

    public List<string> GetUniqueTitleId()
    {
        return _SpiritBeastRepository.GetUniqueSpiritBeastId();
    }
}
