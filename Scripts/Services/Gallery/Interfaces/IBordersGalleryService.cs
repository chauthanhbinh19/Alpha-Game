using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBordersGalleryService
{
    Task<List<Borders>> GetBordersCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBordersCountAsync(string search, string rare);
    Task InsertBorderGalleryAsync(string userId, string Id);
    Task UpdateStatusBorderGalleryAsync(string userId, string Id);
    Task UpdateStarBorderGalleryAsync(string userId, string id, double star);
    Task UpdateBorderGalleryPowerAsync(string userId, string id);
    Task<Borders> SumPowerBordersGalleryAsync(string userId);
}