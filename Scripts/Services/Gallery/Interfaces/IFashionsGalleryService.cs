using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFashionsGalleryService
{
    Task<List<Fashions>> GetFashionsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetFashionsCountAsync(string search, string type, string rare);
    Task InsertFashionGalleryAsync(string Id);
    Task UpdateStatusFashionGalleryAsync(string Id);
    Task UpdateStarFashionGalleryAsync(string Id, double star);
    Task UpdateFashionGalleryPowerAsync(string Id);
    Task<Fashions> SumPowerFashionsGalleryAsync();
}