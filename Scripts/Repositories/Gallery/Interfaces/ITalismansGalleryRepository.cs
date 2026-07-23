using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITalismansGalleryRepository
{
    Task<List<Talismans>> GetTalismansCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetTalismansCountAsync(string search, string type, string rare);
    Task InsertTalismanGalleryAsync(string userId, string Id, Talismans TalismanFromDB);
    Task UpdateStatusTalismanGalleryAsync(string userId, string Id);
    Task UpdateStarTalismanGalleryAsync(string userId, string Id, double star);
    Task UpdateTalismanGalleryPowerAsync(string userId, string Id, Talismans TalismanFromDB);
    Task<Talismans> SumPowerTalismansGalleryAsync(string userId);
}