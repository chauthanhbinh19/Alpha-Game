// using Microsoft.Extensions.Cache.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GameGrowthConfigService : IGameGrowthConfigService
{
    private readonly IGameGrowthConfigRepository _repository;
    // private readonly IMemoryCache _cache;
    private const string CACHE_KEY_ALL_CONFIGS = "GAME_GROWTH_CONFIGS_ALL";

    public GameGrowthConfigService(IGameGrowthConfigRepository repository
        // IMemoryCache cache
    )
    {
        _repository = repository;
        // _cache = cache;
    }

    public static IGameGrowthConfigService Create() => ServiceContainer.GetService<IGameGrowthConfigService>();

    /// <summary>
    /// Lấy danh sách cấu hình từ Cache (hoặc load từ DB nếu chưa cache)
    /// </summary>
    // private async Task<IEnumerable<GameGrowthConfig>> GetCachedConfigsAsync()
    // {
    //     return await _cache.GetOrCreateAsync(CACHE_KEY_ALL_CONFIGS, async entry =>
    //     {
    //         // Cấu hình game tĩnh, cache 24 tiếng hoặc vĩnh viễn đến khi ClearCacheAsync() được gọi
    //         entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24);
    //         return await _repository.GetAllConfigsAsync();
    //     }) ?? Enumerable.Empty<GameGrowthConfig>();
    // }

    /// <summary>
    /// Lấy cụm cấu hình tương ứng với Level/Star hiện tại
    /// </summary>
    public async Task<GameGrowthConfig> GetConfigAsync(string objectType, string growthType, int currentLevel)
    {
        // var configs = await GetCachedConfigsAsync();
        var configs = new List<GameGrowthConfig>();
        return configs.FirstOrDefault(x => x.ObjectType == objectType 
                                       && x.GrowthType == growthType 
                                       && currentLevel >= x.MinLevel 
                                       && currentLevel <= x.MaxLevel);
    }

    /// <summary>
    /// Tính toán EXP yêu cầu theo công thức
    /// </summary>
    public async Task<long> CalculateRequiredExpAsync(string objectType, string growthType, int currentLevel)
    {
        var config = await GetConfigAsync(objectType, growthType, currentLevel);

        if (config == null)
        {
            throw new InvalidOperationException($"Missing Growth Config for ObjectType: '{objectType}', GrowthType: '{growthType}' at Level/Star: {currentLevel}");
        }

        double exp = 0;

        switch (config.FormulaType)
        {
            case "POWER":
                // EXP = base_exp + (level ^ power_factor) * multiplier + (level * linear_factor)
                exp = config.BaseExp + 
                      (Math.Pow(currentLevel, config.PowerFactor) * config.BaseMultiplier) + 
                      (currentLevel * config.LinearFactor);
                break;

            case "ROUND_STEP":
                // Tính EXP thô và làm tròn theo step_size (1, 5, 10, 50, 100...)
                double rawExp = config.BaseExp + 
                                (Math.Pow(currentLevel, config.PowerFactor) * config.BaseMultiplier) + 
                                (currentLevel * config.LinearFactor);
                
                int step = config.StepSize > 0 ? config.StepSize : 1;
                exp = Math.Round(rawExp / step) * step;
                break;

            case "EXPONENTIAL":
                // EXP = base_exp * (multiplier ^ (level - 1))
                exp = config.BaseExp * Math.Pow(config.BaseMultiplier, currentLevel - 1);
                break;

            default:
                exp = config.BaseExp;
                break;
        }

        return (long)Math.Floor(exp);
    }

    /// <summary>
    /// Xóa Cache khi admin cập nhật dữ liệu bảng game_growth_config
    /// </summary>
    public Task ClearCacheAsync()
    {
        // _cache.Remove(CACHE_KEY_ALL_CONFIGS);
        return Task.CompletedTask;
    }
}