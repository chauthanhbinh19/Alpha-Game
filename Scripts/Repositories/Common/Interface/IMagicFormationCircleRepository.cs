using System.Collections.Generic;

public interface IMagicFormationCircleRepository
{
    List<string> GetUniqueMagicFormationCircleTypes();
    List<string> GetUniqueMagicFormationCircleId();
    List<MagicFormationCircles> GetMagicFormationCircle(string type, int pageSize, int offset, string rare);
    int GetMagicFormationCircleCount(string type, string rare);
    List<MagicFormationCircles> GetMagicFormationCircleWithPrice(string type, int pageSize, int offset);
    int GetMagicFormationCircleWithPriceCount(string type);
    MagicFormationCircles GetMagicFormationCircleById(string Id);
    MagicFormationCircles SumPowerMagicFormationCirclePercent();
}
