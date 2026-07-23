using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICoresGalleryService
{
    Task<List<Cores>> GetCoresCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetCoresCountAsync(string search, string rare);
    Task InsertCoreGalleryAsync(string userId, string Id);
    Task UpdateStatusCoreGalleryAsync(string userId, string Id);
    Task UpdateStarCoreGalleryAsync(string userId, string id, double star);
    Task UpdateCoreGalleryPowerAsync(string userId, string id);
    Task<Cores> SumPowerCoresGalleryAsync(string userId);
}