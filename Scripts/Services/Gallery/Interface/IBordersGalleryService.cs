using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBordersGalleryService
{
    Task<List<Borders>> GetBordersCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetBordersCountAsync(string rare);
    Task InsertBorderGalleryAsync(string Id);
    Task UpdateStatusBorderGalleryAsync(string Id);
    Task UpdateStarBorderGalleryAsync(string id, double star);
    Task UpdateBorderGalleryPowerAsync(string id);
    Task<Borders> SumPowerBordersGalleryAsync();
}