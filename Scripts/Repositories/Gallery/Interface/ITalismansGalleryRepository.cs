using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITalismansGalleryRepository
{
    Task<List<Talismans>> GetTalismansCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetTalismansCountAsync(string type, string rare);
    Task InsertTalismanGalleryAsync(string Id, Talismans TalismanFromDB);
    Task UpdateStatusTalismanGalleryAsync(string Id);
    Task UpdateStarTalismanGalleryAsync(string Id, double star);
    Task UpdateTalismanGalleryPowerAsync(string Id, Talismans TalismanFromDB);
    Task<Talismans> SumPowerTalismansGalleryAsync();
}