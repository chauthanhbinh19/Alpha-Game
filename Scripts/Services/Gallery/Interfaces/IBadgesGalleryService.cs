using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBadgesGalleryService
{
    Task<List<Badges>> GetBadgesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBadgesCountAsync(string search, string rare);
    Task InsertBadgeGalleryAsync(string userId, string Id);
    Task UpdateStatusBadgeGalleryAsync(string userId, string Id);
    Task UpdateStarBadgeGalleryAsync(string userId, string id, double star);
    Task UpdateBadgeGalleryPowerAsync(string userId, string id);
    Task<Badges> SumPowerBadgesGalleryAsync(string userId);
}