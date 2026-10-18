using System.Collections.Generic;
using System.Threading.Tasks;

public interface IGameGrowthConfigRepository
{
    Task<GameGrowthConfig?> GetConfigAsync(string objectType, string growthType, int currentLevel);
    Task<IEnumerable<GameGrowthConfig>> GetConfigsByObjectTypeAsync(string objectType);
    Task<IEnumerable<GameGrowthConfig>> GetAllConfigsAsync();
}