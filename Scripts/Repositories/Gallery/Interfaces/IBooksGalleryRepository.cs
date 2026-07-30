using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBooksGalleryRepository
{
    Task<List<Books>> GetBooksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetBooksCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Books>> InsertBookGalleryAsync(string userId, string Id, Books BookFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusBookGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBooksGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarBookGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarBookGalleryAsync(string userId, string bookId);
    Task<InsertOrUpdateResult<List<(string BookId, double CurrentStar)>>> UpdateBatchCurrentStarBooksGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Books>>> InsertBatchBooksGalleryAsync(string userId, List<Books> books);
    Task<Books> GetBookCollectionByIdAsync(string userId, string objectId);
    Task UpdateBookGalleryPowerAsync(string userId, string Id, Books BookFromDB);
    Task<Books> SumPowerBooksGalleryAsync(string userId);
}