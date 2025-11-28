using System.Collections.Generic;

public class MechaBeastsService : IMechaBeastsService
{
    private readonly IMechaBeastsRepository _MechaBeastsRepository;

    public MechaBeastsService(IMechaBeastsRepository titleRepository)
    {
        _MechaBeastsRepository = titleRepository;
    }

    public static MechaBeastsService Create()
    {
        return new MechaBeastsService(new MechaBeastsRepository());
    }

    public List<MechaBeasts> GetMechaBeasts(int pageSize, int offset, string rare)
    {
        List<MechaBeasts> list = _MechaBeastsRepository.GetMechaBeasts(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetMechaBeastsCount(string rare)
    {
        return _MechaBeastsRepository.GetMechaBeastsCount(rare);
    }

    public List<MechaBeasts> GetMechaBeastsWithPrice(int pageSize, int offset)
    {
        List<MechaBeasts> list = _MechaBeastsRepository.GetMechaBeastsWithPrice(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetMechaBeastsWithPriceCount()
    {
        return _MechaBeastsRepository.GetMechaBeastsWithPriceCount();
    }

    public MechaBeasts GetMechaBeastsById(string Id)
    {
        return _MechaBeastsRepository.GetMechaBeastsById(Id);
    }

    public MechaBeasts SumPowerMechaBeastsPercent()
    {
        return _MechaBeastsRepository.SumPowerMechaBeastsPercent();
    }

    public List<string> GetUniqueMechaBeastId()
    {
        return _MechaBeastsRepository.GetUniqueMechaBeastId();
    }
}
