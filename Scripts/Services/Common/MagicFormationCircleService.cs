using System.Collections.Generic;

public class MagicFormationCircleService : IMagicFormationCircleService
{
    private readonly IMagicFormationCircleRepository _magicFormationCircleRepository;

    public MagicFormationCircleService(IMagicFormationCircleRepository magicFormationCircleRepository)
    {
        _magicFormationCircleRepository = magicFormationCircleRepository;
    }

    public static MagicFormationCircleService Create()
    {
        return new MagicFormationCircleService(new MagicFormationCircleRepository());
    }

    public List<string> GetUniqueMagicFormationCircleTypes()
    {
        return _magicFormationCircleRepository.GetUniqueMagicFormationCircleTypes();
    }

    public List<MagicFormationCircle> GetMagicFormationCircle(string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircle> list = _magicFormationCircleRepository.GetMagicFormationCircle(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetMagicFormationCircleCount(string type, string rare)
    {
        return _magicFormationCircleRepository.GetMagicFormationCircleCount(type, rare);
    }

    public List<MagicFormationCircle> GetMagicFormationCircleWithPrice(string type, int pageSize, int offset)
    {
        List<MagicFormationCircle> list = _magicFormationCircleRepository.GetMagicFormationCircleWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetMagicFormationCircleWithPriceCount(string type)
    {
        return _magicFormationCircleRepository.GetMagicFormationCircleWithPriceCount(type);
    }

    public MagicFormationCircle GetMagicFormationCircleById(string Id)
    {
        return _magicFormationCircleRepository.GetMagicFormationCircleById(Id);
    }

    public MagicFormationCircle SumPowerMagicFormationCirclePercent()
    {
        return _magicFormationCircleRepository.SumPowerMagicFormationCirclePercent();
    }

    public List<string> GetUniqueMagicFormationCircleId()
    {
        return _magicFormationCircleRepository.GetUniqueMagicFormationCircleId();
    }
}
