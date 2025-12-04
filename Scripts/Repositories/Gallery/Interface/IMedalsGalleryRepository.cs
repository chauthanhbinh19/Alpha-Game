using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMedalsGalleryRepository
{
    Task<List<Medals>> GetMedalsCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetMedalsCountAsync(string rare);
    Task InsertMedalGalleryAsync(string Id, Medals MedalFromDB);
    Task UpdateStatusMedalGalleryAsync(string Id);
    Task UpdateStarMedalGalleryAsync(string id, double star);
    Task UpdateMedalGalleryPowerAsync(string id, Medals MedalFromDB);
    Task<Medals> SumPowerMedalsGalleryAsync();
}