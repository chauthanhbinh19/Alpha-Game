using System.Collections.Generic;
using System.Threading.Tasks;

public interface IShopsRepository
{
    Task<string> GetShopIdByCodeNameAsync(string shopCodeName);
    Task<List<int>> GetDistinctSequencesAsync(string shopId);
    Task<InsertOrUpdateResult<Shops>> InsertShopAsync(Shops shop);
    Task<InsertOrUpdateResult<Shops>> InsertOrUpdateShopDetailsBatchAsync(Shops shop);
    Task<List<string>> GetShopCodeNamesAsync(string shopType = null);
    Task<List<Currencies>> GetCurrenciesByShopAsync(string userId, ShopRequestDTO shopRequestDTO);
    Task<ShopDTO> GetShopsAsync(ShopRequestDTO shopRequestDTO);
    Task<ShopDTO> GetUserShopsAsync(string userId, ShopRequestDTO shopRequestDTO);
    Task<int> GetShopItemCountAsync(ShopRequestDTO shopRequestDTO);
}