using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBooksGalleryService
{
    Task<List<Books>> GetBooksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetBooksCountAsync(string search, string type, string rare);
    Task InsertBookGalleryAsync(string userId, string Id);
    Task UpdateStatusBookGalleryAsync(string userId, string Id);
    Task UpdateStarBookGalleryAsync(string userId, string Id, double star);
    Task UpdateBookGalleryPowerAsync(string userId, string Id);
    Task<Books> SumPowerBooksGalleryAsync(string userId);
}