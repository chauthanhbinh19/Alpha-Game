using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBeveragesGalleryRepository
{
    Task<List<Beverages>> GetBeveragesCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetBeveragesCountAsync(string rare);
    Task InsertBeverageGalleryAsync(string Id, Beverages BeverageFromDB);
    Task UpdateStatusBeverageGalleryAsync(string Id);
    Task UpdateStarBeverageGalleryAsync(string id, double star);
    Task UpdateBeverageGalleryPowerAsync(string id, Beverages BeverageFromDB);
    Task<Beverages> SumPowerBeveragesGalleryAsync();
}