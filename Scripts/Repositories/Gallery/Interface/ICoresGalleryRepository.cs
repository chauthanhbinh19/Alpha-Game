using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICoresGalleryRepository
{
    Task<List<Cores>> GetCoresCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetCoresCountAsync(string rare);
    Task InsertCoreGalleryAsync(string Id, Cores CoreFromDB);
    Task UpdateStatusCoreGalleryAsync(string Id);
    Task UpdateStarCoreGalleryAsync(string id, double star);
    Task UpdateCoreGalleryPowerAsync(string id, Cores CoreFromDB);
    Task<Cores> SumPowerCoresGalleryAsync();
}