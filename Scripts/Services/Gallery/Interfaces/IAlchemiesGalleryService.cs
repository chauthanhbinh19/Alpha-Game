using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAlchemiesGalleryService
{
    Task<List<Alchemies>> GetAlchemyCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetAlchemyCountAsync(string search, string type, string rare);
    Task InsertAlchemyGalleryAsync(string Id);
    Task UpdateStatusAlchemyGalleryAsync(string Id);
    Task UpdateStarAlchemyGalleryAsync(string Id, double star);
    Task UpdateAlchemyGalleryPowerAsync(string Id);
    Task<Alchemies> SumPowerAlchemyGalleryAsync();
}
