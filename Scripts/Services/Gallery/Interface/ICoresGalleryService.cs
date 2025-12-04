using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICoresGalleryService
{
    Task<List<Cores>> GetCoresCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetCoresCountAsync(string rare);
    Task InsertCoreGalleryAsync(string Id);
    Task UpdateStatusCoreGalleryAsync(string Id);
    Task UpdateStarCoreGalleryAsync(string id, double star);
    Task UpdateCoreGalleryPowerAsync(string id);
    Task<Cores> SumPowerCoresGalleryAsync();
}