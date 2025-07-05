using System.Collections.Generic;

public interface IMagicFormationCircleService
{
    List<string> GetUniqueMagicFormationCircleTypes();
    List<string> GetUniqueMagicFormationCircleId();
    List<MagicFormationCircle> GetMagicFormationCircle(string type, int pageSize, int offset);
    int GetMagicFormationCircleCount(string type);
    List<MagicFormationCircle> GetMagicFormationCircleWithPrice(string type, int pageSize, int offset);
    int GetMagicFormationCircleWithPriceCount(string type);
    MagicFormationCircle GetMagicFormationCircleById(string Id);
    MagicFormationCircle SumPowerMagicFormationCirclePercent();
}
