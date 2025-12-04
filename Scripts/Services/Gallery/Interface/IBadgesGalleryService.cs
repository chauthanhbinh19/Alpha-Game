using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBadgesGalleryService
{
    Task<List<Badges>> GetBadgesCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetBadgesCountAsync(string rare);
    Task InsertBadgeGalleryAsync(string Id);
    Task UpdateStatusBadgeGalleryAsync(string Id);
    Task UpdateStarBadgeGalleryAsync(string id, double star);
    Task UpdateBadgeGalleryPowerAsync(string id);
    Task<Badges> SumPowerBadgesGalleryAsync();
}