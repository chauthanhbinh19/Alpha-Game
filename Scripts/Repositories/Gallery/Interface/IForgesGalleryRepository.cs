using System.Collections.Generic;
using System.Threading.Tasks;

public interface IForgesGalleryRepository
{
    Task<List<Forges>> GetForgesCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetForgesCountAsync(string type, string rare);
    Task InsertForgeGalleryAsync(string Id, Forges ForgeFromDB);
    Task UpdateStatusForgeGalleryAsync(string Id);
    Task UpdateStarForgeGalleryAsync(string Id, double star);
    Task UpdateForgeGalleryPowerAsync(string Id, Forges ForgeFromDB);
    Task<Forges> SumPowerForgesGalleryAsync();
}