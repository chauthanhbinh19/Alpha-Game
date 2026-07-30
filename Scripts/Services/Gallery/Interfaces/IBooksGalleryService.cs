using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBooksGalleryService
{
    Task<List<Books>> GetBooksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetBooksCountAsync(string search, string type, string rare);
    Task<bool> InsertBookGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusBookGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusBooksGalleryAsync(string userId);
    Task<bool> UpdateStarBookGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarBookGalleryAsync(string userId, string bookId);
    Task<bool> UpdateBatchCurrentStarBooksGalleryAsync(string userId);
    Task<bool> InsertBatchBooksGalleryAsync(string userId, List<Books> books);
    Task<Books> GetBookCollectionByIdAsync(string userId, string objectId);
    Task UpdateBookGalleryPowerAsync(string userId, string Id);
    Task<Books> SumPowerBooksGalleryAsync(string userId);
}