using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBadgesGalleryRepository
{
    Task<List<Badges>> GetBadgesCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetBadgesCountAsync(string search, string rare);
    Task InsertBadgeGalleryAsync(string Id, Badges BadgeFromDB);
    Task UpdateStatusBadgeGalleryAsync(string Id);
    Task UpdateStarBadgeGalleryAsync(string id, double star);
    Task UpdateBadgeGalleryPowerAsync(string id, Badges BadgeFromDB);
    Task<Badges> SumPowerBadgesGalleryAsync();
}