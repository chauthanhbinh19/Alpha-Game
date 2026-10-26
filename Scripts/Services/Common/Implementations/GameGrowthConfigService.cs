using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class GameGrowthConfigService : IGameGrowthConfigService
{
    private readonly IGameGrowthConfigRepository _repository;
    private readonly SemaphoreSlim _cacheLock = new SemaphoreSlim(1, 1);
    private readonly Dictionary<string, List<GameGrowthConfig>> _growthConfigCache =
        new Dictionary<string, List<GameGrowthConfig>>(StringComparer.Ordinal);

    public GameGrowthConfigService(IGameGrowthConfigRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public static IGameGrowthConfigService Create() => ServiceContainer.GetService<IGameGrowthConfigService>();

    public async Task<GameGrowthConfig> GetConfigAsync(string objectType, string growthType, int currentLevel)
    {
        if (string.IsNullOrWhiteSpace(objectType))
            throw new ArgumentException("Object type is required.", nameof(objectType));
        if (string.IsNullOrWhiteSpace(growthType))
            throw new ArgumentException("Feature code name is required.", nameof(growthType));
        if (currentLevel < 1)
            throw new ArgumentOutOfRangeException(nameof(currentLevel), "Level must be greater than zero.");

        List<GameGrowthConfig> configs = await GetGrowthConfigsAsync(objectType);
        List<GameGrowthConfig> matchingConfigs = configs
            .Where(config =>
                string.Equals(config.GrowthType, growthType, StringComparison.Ordinal) &&
                currentLevel >= config.MinLevel &&
                currentLevel <= config.MaxLevel)
            .ToList();

        if (matchingConfigs.Count == 0)
        {
            throw new InvalidOperationException(
                $"Missing Growth Config for ObjectType: '{objectType}', GrowthType: '{growthType}' at Level: {currentLevel}.");
        }

        int selectedMinLevel = matchingConfigs.Max(config => config.MinLevel);
        List<GameGrowthConfig> mostSpecificConfigs = matchingConfigs
            .Where(config => config.MinLevel == selectedMinLevel)
            .ToList();

        if (mostSpecificConfigs.Count > 1)
        {
            throw new InvalidOperationException(
                $"Overlapping Growth Config ranges for ObjectType: '{objectType}', GrowthType: '{growthType}' at Level: {currentLevel}.");
        }

        return mostSpecificConfigs[0];
    }

    public async Task<long> CalculateRequiredExpAsync(string objectType, string growthType, int currentLevel)
    {
        GameGrowthConfig config = await GetConfigAsync(objectType, growthType, currentLevel);

        double rawExp;
        switch (config.FormulaType)
        {
            case "POWER":
                rawExp = config.BaseExp +
                         (Math.Pow(currentLevel, config.PowerFactor) * config.BaseMultiplier) +
                         (currentLevel * config.LinearFactor);
                break;

            case "ROUND_STEP":
                double unroundedExp = config.BaseExp +
                                      (Math.Pow(currentLevel, config.PowerFactor) * config.BaseMultiplier) +
                                      (currentLevel * config.LinearFactor);
                int step = config.StepSize > 0 ? config.StepSize : 1;
                rawExp = Math.Round(unroundedExp / step) * step;
                break;

            case "EXPONENTIAL":
                rawExp = config.BaseExp * Math.Pow(config.BaseMultiplier, currentLevel - 1);
                break;

            default:
                throw new InvalidOperationException(
                    $"Unsupported growth formula '{config.FormulaType}' for config '{config.Id}'.");
        }

        if (double.IsNaN(rawExp) || double.IsInfinity(rawExp) || rawExp < 0 || rawExp > long.MaxValue)
        {
            throw new InvalidOperationException(
                $"Growth formula '{config.FormulaType}' returned invalid EXP '{rawExp}' for level {currentLevel}.");
        }

        return (long)Math.Floor(rawExp);
    }

    public async Task ClearCacheAsync()
    {
        await _cacheLock.WaitAsync();
        try
        {
            _growthConfigCache.Clear();
        }
        finally
        {
            _cacheLock.Release();
        }
    }

    private async Task<List<GameGrowthConfig>> GetGrowthConfigsAsync(string objectType)
    {
        await _cacheLock.WaitAsync();
        try
        {
            if (_growthConfigCache.TryGetValue(objectType, out List<GameGrowthConfig> cachedConfigs))
                return cachedConfigs;

            IEnumerable<GameGrowthConfig> configs = await _repository.GetConfigsByObjectTypeAsync(objectType);
            List<GameGrowthConfig> configList = configs?.ToList() ?? new List<GameGrowthConfig>();
            _growthConfigCache.Add(objectType, configList);
            return configList;
        }
        finally
        {
            _cacheLock.Release();
        }
    }
}
