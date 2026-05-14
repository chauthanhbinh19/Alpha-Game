using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class GachaRatesConfig
{
    public static readonly Dictionary<string, double> DefaultMainRates =
        new()
        {
            { AppConstants.MainType.ITEM, 50 }
        };

    public static async Task<Dictionary<string, double>> GetMainRatesAsync(string mainType)
    {
        return new Dictionary<string, double>
        {
            { mainType, 50 },
            { AppConstants.MainType.ITEM, 50 }
        };
    }

    public static async Task<Dictionary<string, double>> GetCardTypeRatesAsync(string mainType)
    {
        var types = await TypeManager.GetUniqueTypesAsync(mainType);

        if (!types.Any())
            return new Dictionary<string, double>();

        return types.ToDictionary(
            x => x,
            x => 100.0 / types.Count
        );
    }

    public static async Task<Dictionary<string, double>> GetItemTypeRatesAsync()
    {
        var types = await TypeManager.GetUniqueTypesAsync(
            AppConstants.MainType.ITEM
        );

        var validTypes = types
            .Where(x => AppConstants.Type.GachaItemTypes.Contains(x))
            .ToList();

        if (!validTypes.Any())
            return new Dictionary<string, double>();

        return validTypes.ToDictionary(
            x => x,
            x => 100.0 / validTypes.Count
        );
    }
}