using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAlchemiesGalleryRepository
{
    Task<List<Alchemies>> GetAlchemyCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetAlchemyCountAsync(string type, string rare);
    Task InsertAlchemyGalleryAsync(string Id, Alchemies AlchemyFromDB);
    Task UpdateStatusAlchemyGalleryAsync(string Id);
    Task UpdateStarAlchemyGalleryAsync(string Id, double star);
    Task UpdateAlchemyGalleryPowerAsync(string Id, Alchemies AlchemyFromDB);
    Task<Alchemies> SumPowerAlchemyGalleryAsync();
}
