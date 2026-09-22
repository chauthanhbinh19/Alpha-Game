using System.Collections.Generic;
using System.Threading.Tasks;
public interface IGameItemCatalogService
{
    Task<List<GameItemCatalog>> GetGameItemCatalogAsync(string search, string objectType, string rare, int pageSize, int offset);
    Task<List<GameItemCatalog>> GetGameItemCatalogSimpleAsync();
    Task<List<GameItemCatalog>> GetGameItemCatalogWithoutLimitAsync();
    Task<GameItemCatalog> GetGameItemCatalogByIdAsync(string objectId);
}