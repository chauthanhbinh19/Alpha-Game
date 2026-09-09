using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBooksGalleryService
{
    Task<List<Books>> GetBooksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetBooksCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertBookGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusBookGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBooksGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarBookGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarBookGalleryAsync(string userId, string bookId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarBooksGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchBooksGalleryAsync(string userId, List<Books> books);
    Task<Books> GetBookCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateBookGalleryPowerAsync(string userId, string Id);
    Task<Books> SumPowerBooksGalleryAsync(string userId);
}