using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBooksGalleryService
{
    Task<List<Books>> GetBooksCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetBooksCountAsync(string type, string rare);
    Task InsertBookGalleryAsync(string Id);
    Task UpdateStatusBookGalleryAsync(string Id);
    Task UpdateStarBookGalleryAsync(string Id, double star);
    Task UpdateBookGalleryPowerAsync(string Id);
    Task<Books> SumPowerBooksGalleryAsync();
}