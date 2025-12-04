using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMedalsGalleryService
{
    Task<List<Medals>> GetMedalsCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetMedalsCountAsync(string rare);
    Task InsertMedalGalleryAsync(string Id);
    Task UpdateStatusMedalGalleryAsync(string Id);
    Task UpdateStarMedalGalleryAsync(string id, double star);
    Task UpdateMedalGalleryPowerAsync(string id);
    Task<Medals> SumPowerMedalsGalleryAsync();
}