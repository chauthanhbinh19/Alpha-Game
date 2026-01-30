using System.Collections.Generic;
using System.Threading.Tasks;

public interface IForgesGalleryService
{
    Task<List<Forges>> GetForgesCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetForgesCountAsync(string search, string type, string rare);
    Task InsertForgeGalleryAsync(string Id);
    Task UpdateStatusForgeGalleryAsync(string Id);
    Task UpdateStarForgeGalleryAsync(string Id, double star);
    Task UpdateForgeGalleryPowerAsync(string Id);
    Task<Forges> SumPowerForgesGalleryAsync();
}