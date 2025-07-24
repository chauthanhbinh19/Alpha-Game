using System.Collections.Generic;

public interface IMagicFormationCircleRepository
{
    List<string> GetUniqueMagicFormationCircleTypes();
    List<string> GetUniqueMagicFormationCircleId();
    List<MagicFormationCircle> GetMagicFormationCircle(string type, int pageSize, int offset, string rare);
    int GetMagicFormationCircleCount(string type, string rare);
    List<MagicFormationCircle> GetMagicFormationCircleWithPrice(string type, int pageSize, int offset);
    int GetMagicFormationCircleWithPriceCount(string type);
    MagicFormationCircle GetMagicFormationCircleById(string Id);
    MagicFormationCircle SumPowerMagicFormationCirclePercent();
}
