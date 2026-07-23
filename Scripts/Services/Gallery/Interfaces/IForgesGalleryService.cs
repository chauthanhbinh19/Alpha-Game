using System.Collections.Generic;
using System.Threading.Tasks;

public interface IForgesGalleryService
{
    Task<List<Forges>> GetForgesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetForgesCountAsync(string search, string type, string rare);
    Task InsertForgeGalleryAsync(string userId, string Id);
    Task UpdateStatusForgeGalleryAsync(string userId, string Id);
    Task UpdateStarForgeGalleryAsync(string userId, string Id, double star);
    Task UpdateForgeGalleryPowerAsync(string userId, string Id);
    Task<Forges> SumPowerForgesGalleryAsync(string userId);
}