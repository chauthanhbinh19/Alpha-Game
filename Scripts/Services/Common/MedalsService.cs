using System.Collections.Generic;

public class MedalsService : IMedalsService
{
    private readonly IMedalsRepository _medalsRepository;

    public MedalsService(IMedalsRepository medalsRepository)
    {
        _medalsRepository = medalsRepository;
    }

    public static MedalsService Create()
    {
        return new MedalsService(new MedalsRepository());
    }

    public List<Medals> GetMedals(int pageSize, int offset)
    {
        List<Medals> list = _medalsRepository.GetMedals(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetMedalsCount()
    {
        return _medalsRepository.GetMedalsCount();
    }

    public List<Medals> GetMedalsWithPrice(int pageSize, int offset)
    {
        List<Medals> list = _medalsRepository.GetMedalsWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetMedalsWithPriceCount()
    {
        return _medalsRepository.GetMedalsWithPriceCount();
    }

    public Medals GetMedalsById(string Id)
    {
        return _medalsRepository.GetMedalsById(Id);
    }

    public Medals SumPowerMedalsPercent()
    {
        return _medalsRepository.SumPowerMedalsPercent();
    }

    public List<string> GetUniqueMedalId()
    {
        return _medalsRepository.GetUniqueMedalId();
    }
}
