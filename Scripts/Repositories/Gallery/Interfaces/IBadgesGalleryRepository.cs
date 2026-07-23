using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBadgesGalleryRepository
{
    Task<List<Badges>> GetBadgesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBadgesCountAsync(string search, string rare);
    Task InsertBadgeGalleryAsync(string userId, string Id, Badges BadgeFromDB);
    Task UpdateStatusBadgeGalleryAsync(string userId, string Id);
    Task UpdateStarBadgeGalleryAsync(string userId, string id, double star);
    Task UpdateBadgeGalleryPowerAsync(string userId, string id, Badges BadgeFromDB);
    Task<Badges> SumPowerBadgesGalleryAsync(string userId);
}