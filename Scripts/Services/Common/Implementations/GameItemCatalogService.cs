using System.Collections.Generic;
using System.Threading.Tasks;
public class GameItemCatalogService : IGameItemCatalogService
{
    private readonly IGameItemCatalogRepository _gameItemCatalogRepository;

    public GameItemCatalogService(IGameItemCatalogRepository gameItemCatalogRepository)
    {
        _gameItemCatalogRepository = gameItemCatalogRepository;
    }

    public static IGameItemCatalogService Create() => ServiceContainer.GetService<IGameItemCatalogService>();

    public Task<List<GameItemCatalog>> GetGameItemCatalogAsync(string search, string objectType, string rare, int pageSize, int offset)
    {
        return _gameItemCatalogRepository.GetGameItemCatalogAsync(search, objectType, rare, pageSize, offset);
    }

    public Task<List<GameItemCatalog>> GetGameItemCatalogSimpleAsync()
    {
        return _gameItemCatalogRepository.GetGameItemCatalogSimpleAsync();
    }

    public Task<List<GameItemCatalog>> GetGameItemCatalogWithoutLimitAsync()
    {
        return _gameItemCatalogRepository.GetGameItemCatalogWithoutLimitAsync();
    }

    Task<GameItemCatalog> IGameItemCatalogService.GetGameItemCatalogByIdAsync(string objectId)
    {
        return _gameItemCatalogRepository.GetGameItemCatalogByIdAsync(objectId);
    }
}