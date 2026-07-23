using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMedalsGalleryService
{
    Task<List<Medals>> GetMedalsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetMedalsCountAsync(string search, string rare);
    Task InsertMedalGalleryAsync(string userId, string Id);
    Task UpdateStatusMedalGalleryAsync(string userId, string Id);
    Task UpdateStarMedalGalleryAsync(string userId, string id, double star);
    Task UpdateMedalGalleryPowerAsync(string userId, string id);
    Task<Medals> SumPowerMedalsGalleryAsync(string userId);
}