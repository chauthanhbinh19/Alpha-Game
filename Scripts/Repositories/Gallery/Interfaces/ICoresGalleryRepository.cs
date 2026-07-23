using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICoresGalleryRepository
{
    Task<List<Cores>> GetCoresCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetCoresCountAsync(string search, string rare);
    Task InsertCoreGalleryAsync(string userId, string Id, Cores CoreFromDB);
    Task UpdateStatusCoreGalleryAsync(string userId, string Id);
    Task UpdateStarCoreGalleryAsync(string userId, string id, double star);
    Task UpdateCoreGalleryPowerAsync(string userId, string id, Cores CoreFromDB);
    Task<Cores> SumPowerCoresGalleryAsync(string userId);
}