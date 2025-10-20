using System.Collections.Generic;

public class SpiritCardService : ISpiritCardService
{
    private readonly ISpiritCardRepository _SpiritCardRepository;

    public SpiritCardService(ISpiritCardRepository titleRepository)
    {
        _SpiritCardRepository = titleRepository;
    }

    public static SpiritCardService Create()
    {
        return new SpiritCardService(new SpiritCardRepository());
    }

    public List<string> GetUniqueSpiritCardTypes()
    {
        return _SpiritCardRepository.GetUniqueSpiritCardTypes();
    }

    public List<SpiritCards> GetSpiritCard(string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> list = _SpiritCardRepository.GetSpiritCard(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetSpiritCardCount(string type, string rare)
    {
        return _SpiritCardRepository.GetSpiritCardCount(type, rare);
    }

    public List<SpiritCards> GetSpiritCardWithPrice(string type, int pageSize, int offset)
    {
        List<SpiritCards> list = _SpiritCardRepository.GetSpiritCardWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetSpiritCardWithPriceCount(string type)
    {
        return _SpiritCardRepository.GetSpiritCardWithPriceCount(type);
    }

    public SpiritCards GetSpiritCardById(string Id)
    {
        return _SpiritCardRepository.GetSpiritCardById(Id);
    }

    public SpiritCards SumPowerSpiritCardPercent()
    {
        return _SpiritCardRepository.SumPowerSpiritCardPercent();
    }

    public List<string> GetUniqueTitleId()
    {
        return _SpiritCardRepository.GetUniqueSpiritCardId();
    }
}
