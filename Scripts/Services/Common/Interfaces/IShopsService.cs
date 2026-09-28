using System.Collections.Generic;
using System.Threading.Tasks;

public interface IShopsService
{
    Task<string> GetShopIdByCodeNameAsync(string shopCodeName);
    Task<List<int>> GetDistinctSequencesAsync(string shopId);
    Task<InsertOrUpdateResult<bool>> InsertShopAsync(Shops shop);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateShopDetailsBatchAsync(Shops shop);
    Task<List<string>> GetShopCodeNamesAsync(string shopType = null);
    Task<List<Currencies>> GetCurrenciesByShopAsync(string userId, ShopRequestDTO shopRequestDTO);
    Task<ShopDTO> GetShopsAsync(ShopRequestDTO shopRequestDTO);
    Task<ShopDTO> GetUserShopsAsync(string userId, ShopRequestDTO shopRequestDTO);
    Task<int> GetShopItemCountAsync(ShopRequestDTO shopRequestDTO);
}