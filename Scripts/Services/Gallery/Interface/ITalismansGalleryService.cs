using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITalismansGalleryService
{
    Task<List<Talismans>> GetTalismansCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetTalismansCountAsync(string search, string type, string rare);
    Task InsertTalismanGalleryAsync(string Id);
    Task UpdateStatusTalismanGalleryAsync(string Id);
    Task UpdateStarTalismanGalleryAsync(string Id, double star);
    Task UpdateTalismanGalleryPowerAsync(string Id);
    Task<Talismans> SumPowerTalismansGalleryAsync();
}