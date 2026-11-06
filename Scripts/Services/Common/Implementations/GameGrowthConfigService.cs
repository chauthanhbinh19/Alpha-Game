// using Microsoft.Extensions.Cache.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GameGrowthConfigService : IGameGrowthConfigService
{
    private readonly IGameGrowthConfigRepository _repository;
    private List<GameGrowthConfig> _cachedConfigs;

    public GameGrowthConfigService(IGameGrowthConfigRepository repository)
    {
        _repository = repository;
    }

    public static IGameGrowthConfigService Create() => ServiceContainer.GetService<IGameGrowthConfigService>();

    /// <summary>
    /// Lấy danh sách cấu hình từ Cache (hoặc load từ DB nếu chưa cache)
    /// </summary>
    private async Task<List<GameGrowthConfig>> GetCachedConfigsAsync()
    {
        if (_cachedConfigs == null)
        {
            _cachedConfigs = (await _repository.GetAllConfigsAsync()).ToList();
        }

        return _cachedConfigs;
    }

    /// <summary>
    /// Lấy cụm cấu hình tương ứng với Level/Star hiện tại
    /// </summary>
    public async Task<List<GameGrowthConfig>> GetConfigAsync(string objectType, string growthType, int currentLevel)
    {
        var configs = await GetCachedConfigsAsync();
        return configs;
    }

    public async Task<List<GameGrowthConfig>> GetConfigsByObjectTypeAsync(string objectType)
    {
        var configs = await GetCachedConfigsAsync();
        return configs
            .Where(config => string.Equals(config.ObjectType, objectType, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Tính toán EXP yêu cầu theo công thức
    /// </summary>
    // public async Task<long> CalculateRequiredExpAsync(string objectType, string growthType, int currentLevel)
    // {
    //     double exp = 0;

    //     switch (config.FormulaType)
    //     {
    //         case "POWER":
    //             // EXP = base_exp + (level ^ power_factor) * multiplier + (level * linear_factor)
    //             exp = config.BaseExp + 
    //                   (Math.Pow(currentLevel, config.PowerFactor) * config.BaseMultiplier) + 
    //                   (currentLevel * config.LinearFactor);
    //             break;

    //         case "ROUND_STEP":
    //             // Tính EXP thô và làm tròn theo step_size (1, 5, 10, 50, 100...)
    //             double rawExp = config.BaseExp + 
    //                             (Math.Pow(currentLevel, config.PowerFactor) * config.BaseMultiplier) + 
    //                             (currentLevel * config.LinearFactor);
                
    //             int step = config.StepSize > 0 ? config.StepSize : 1;
    //             exp = Math.Round(rawExp / step) * step;
    //             break;

    //         case "EXPONENTIAL":
    //             // EXP = base_exp * (multiplier ^ (level - 1))
    //             exp = config.BaseExp * Math.Pow(config.BaseMultiplier, currentLevel - 1);
    //             break;

    //         default:
    //             exp = config.BaseExp;
    //             break;
    //     }

    //     return (long)Math.Floor(exp);
    // }

    /// <summary>
    /// Xóa Cache khi admin cập nhật dữ liệu bảng game_growth_config
    /// </summary>
    public Task ClearCacheAsync()
    {
        _cachedConfigs = null;
        return Task.CompletedTask;
    }
}