using System.Collections.Generic;
using System.Threading.Tasks;

public interface IGameConfigRepository
{
    Task<GameConfig> GetGameConfigAsync(string objectType, string objectColumn);
    Task<List<GameConfig>> GetAllGameConfigsAsync();
}