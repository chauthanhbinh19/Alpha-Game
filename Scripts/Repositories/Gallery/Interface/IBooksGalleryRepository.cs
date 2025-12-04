using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBooksGalleryRepository
{
    Task<List<Books>> GetBooksCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetBooksCountAsync(string type, string rare);
    Task InsertBookGalleryAsync(string Id, Books BookFromDB);
    Task UpdateStatusBookGalleryAsync(string Id);
    Task UpdateStarBookGalleryAsync(string Id, double star);
    Task UpdateBookGalleryPowerAsync(string Id, Books BookFromDB);
    Task<Books> SumPowerBooksGalleryAsync();
}