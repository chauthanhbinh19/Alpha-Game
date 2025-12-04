using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRelicsGalleryService
{
    Task<List<Relics>> GetRelicsCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetRelicsCountAsync(string type, string rare);
    Task InsertRelicGalleryAsync(string Id);
    Task UpdateStatusRelicGalleryAsync(string Id);
    Task UpdateStarRelicGalleryAsync(string Id, double star);
    Task UpdateRelicGalleryPowerAsync(string Id);
    Task<Relics> SumPowerRelicsGalleryAsync();
}