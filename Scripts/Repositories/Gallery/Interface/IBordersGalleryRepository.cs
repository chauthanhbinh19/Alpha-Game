using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBordersGalleryRepository
{
    Task<List<Borders>> GetBordersCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetBordersCountAsync(string search, string rare);
    Task InsertBorderGalleryAsync(string Id, Borders BorderFromDB);
    Task UpdateStatusBorderGalleryAsync(string Id);
    Task UpdateStarBorderGalleryAsync(string id, double star);
    Task UpdateBorderGalleryPowerAsync(string id, Borders BorderFromDB);
    Task<Borders> SumPowerBordersGalleryAsync();
}