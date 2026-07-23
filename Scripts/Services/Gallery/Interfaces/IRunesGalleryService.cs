using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRunesGalleryService
{
    Task<List<Runes>> GetRunesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetRunesCountAsync(string search, string rare);
    Task InsertRuneGalleryAsync(string userId, string Id);
    Task UpdateStatusRuneGalleryAsync(string userId, string Id);
    Task UpdateStarRuneGalleryAsync(string userId, string id, double star);
    Task UpdateRuneGalleryPowerAsync(string userId, string id);
    Task<Runes> SumPowerRunesGalleryAsync(string userId);
}