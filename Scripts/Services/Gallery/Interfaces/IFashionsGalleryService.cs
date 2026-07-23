using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFashionsGalleryService
{
    Task<List<Fashions>> GetFashionsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetFashionsCountAsync(string search, string type, string rare);
    Task InsertFashionGalleryAsync(string userId, string Id);
    Task UpdateStatusFashionGalleryAsync(string userId, string Id);
    Task UpdateStarFashionGalleryAsync(string userId, string Id, double star);
    Task UpdateFashionGalleryPowerAsync(string userId, string Id);
    Task<Fashions> SumPowerFashionsGalleryAsync(string userId);
}