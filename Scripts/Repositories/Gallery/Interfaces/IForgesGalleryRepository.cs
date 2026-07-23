using System.Collections.Generic;
using System.Threading.Tasks;

public interface IForgesGalleryRepository
{
    Task<List<Forges>> GetForgesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetForgesCountAsync(string search, string type, string rare);
    Task InsertForgeGalleryAsync(string userId, string Id, Forges ForgeFromDB);
    Task UpdateStatusForgeGalleryAsync(string userId, string Id);
    Task UpdateStarForgeGalleryAsync(string userId, string Id, double star);
    Task UpdateForgeGalleryPowerAsync(string userId, string Id, Forges ForgeFromDB);
    Task<Forges> SumPowerForgesGalleryAsync(string userId);
}