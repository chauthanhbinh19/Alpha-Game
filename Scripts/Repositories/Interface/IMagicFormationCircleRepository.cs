using System.Collections.Generic;

public interface IMagicFormationCircleRepository
{
    List<string> GetUniqueMagicFormationCircleTypes();
    List<MagicFormationCircle> GetMagicFormationCircle(string type, int pageSize, int offset);
    int GetMagicFormationCircleCount(string type);
    List<MagicFormationCircle> GetMagicFormationCircleWithPrice(string type, int pageSize, int offset);
    int GetMagicFormationCircleWithPriceCount(string type);
    MagicFormationCircle GetMagicFormationCircleById(string Id);
    MagicFormationCircle SumPowerMagicFormationCirclePercent();
}
