using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBooksGalleryRepository
{
    Task<List<Books>> GetBooksCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetBooksCountAsync(string search, string type, string rare);
    Task InsertBookGalleryAsync(string userId, string Id, Books BookFromDB);
    Task UpdateStatusBookGalleryAsync(string userId, string Id);
    Task UpdateStarBookGalleryAsync(string userId, string Id, double star);
    Task UpdateBookGalleryPowerAsync(string userId, string Id, Books BookFromDB);
    Task<Books> SumPowerBooksGalleryAsync(string userId);
}