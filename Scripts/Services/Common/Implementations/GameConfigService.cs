using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GameConfigService : IGameConfigService
{
    private readonly IGameConfigRepository _repository;

    public GameConfigService(IGameConfigRepository repository)
    {
        _repository = repository;
    }

    public static IGameConfigService Create() => ServiceContainer.GetService<IGameConfigService>();

    public async Task<GameConfig> GetGameConfigAsync(string objectType, string objectColumn)
    {
        return await _repository.GetGameConfigAsync(objectType, objectColumn);
    }

    public async Task<List<GameConfig>> GetAllGameConfigsAsync()
    {
        return await _repository.GetAllGameConfigsAsync();
    }
}