using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAlchemiesGalleryRepository
{
    Task<List<Alchemies>> GetAlchemiesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetAlchemyCountAsync(string search, string type, string rare);
    Task InsertAlchemyGalleryAsync(string userId, string Id, Alchemies AlchemyFromDB);
    Task UpdateStatusAlchemyGalleryAsync(string userId, string Id);
    Task UpdateStarAlchemyGalleryAsync(string userId, string Id, double star);
    Task UpdateAlchemyGalleryPowerAsync(string userId, string Id, Alchemies AlchemyFromDB);
    Task<Alchemies> SumPowerAlchemyGalleryAsync(string userId);
}
