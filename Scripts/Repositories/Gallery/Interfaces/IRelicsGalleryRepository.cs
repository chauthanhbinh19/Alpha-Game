using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRelicsGalleryRepository
{
    Task<List<Relics>> GetRelicsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetRelicsCountAsync(string search, string type, string rare);
    Task InsertRelicGalleryAsync(string userId, string Id, Relics RelicFromDB);
    Task UpdateStatusRelicGalleryAsync(string userId, string Id);
    Task UpdateStarRelicGalleryAsync(string userId, string Id, double star);
    Task UpdateRelicGalleryPowerAsync(string userId, string Id, Relics RelicFromDB);
    Task<Relics> SumPowerRelicsGalleryAsync(string userId);
}